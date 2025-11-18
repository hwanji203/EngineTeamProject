using UnityEngine;
using System.Collections.Generic;

public enum VFXType
{
    None,
    Swing,
    DashBoost,
    Hit,
    DashAttack,
    EnemyDead,
}

public class VFXObject : MonoBehaviour
{
    private ParticleSystem particle;
    private Animator animator;
    private float lifetime;
    private float timer;
    private bool active;

    public System.Action<VFXObject> OnDeactivate;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Play(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
        active = true;
        timer = 0f;

        if (particle)
        {
            particle.Play();
            lifetime = particle.main.duration + particle.main.startLifetime.constantMax;
        }
        else if (animator)
        {
            animator.Play(0);
            lifetime = GetAnimationClipLength(animator);
        }
        else
        {
            lifetime = 1f;
        }
    }

    private void Update()
    {
        if (!active) return;

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        active = false;
        gameObject.SetActive(false);
        OnDeactivate?.Invoke(this);
    }

    private float GetAnimationClipLength(Animator animator)
    {
        if (animator.runtimeAnimatorController == null ||
            animator.runtimeAnimatorController.animationClips.Length == 0)
            return 1f;

        return animator.runtimeAnimatorController.animationClips[0].length;
    }
}

[System.Serializable]
public class VFXPool
{
    public VFXType type;
    public GameObject prefab;
    public int initialCount = 5;

    [HideInInspector] public Queue<VFXObject> poolQueue = new();
}

public class VFXManager : MonoSingleton<VFXManager>
{

    [SerializeField] private List<VFXPool> vfxPools = new();
    private Dictionary<VFXType, VFXPool> vfxDict = new();

    protected override void Awake()
    {
        base.Awake();
        InitializePools();
    }


    private void InitializePools()
    {
        foreach (var pool in vfxPools)
        {
            var parent = new GameObject($"{pool.type}_Pool").transform;
            parent.SetParent(transform);

            for (int i = 0; i < pool.initialCount; i++)
            {
                var obj = Instantiate(pool.prefab, parent);

                var vfxObj = obj.GetComponent<VFXObject>();
                if (vfxObj == null)
                    vfxObj = obj.AddComponent<VFXObject>();

                obj.SetActive(false);
                vfxObj.OnDeactivate += ReturnToPool;
                pool.poolQueue.Enqueue(vfxObj);
            }

            try
            {
                vfxDict.Add(pool.type, pool);
            }
            catch (System.ArgumentException)
            {
                Debug.LogWarning("중복 등록");
            }
        }
    }

    public GameObject Play(VFXType type, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!vfxDict.ContainsKey(type))
        {
            Debug.LogWarning($"VFX {type} not registered!");
            return null;
        }

        var pool = vfxDict[type];
        VFXObject obj;

        if (pool.poolQueue.Count > 0)
        {
            obj = pool.poolQueue.Dequeue();
            obj.transform.SetParent(parent);
        }
        else
        {
            if (parent == null) parent = transform;
            var newObj = Instantiate(pool.prefab, parent);
            obj = newObj.GetComponent<VFXObject>();
            if (obj == null)
                obj = newObj.AddComponent<VFXObject>();
            obj.OnDeactivate += ReturnToPool;
        }

        obj.Play(position, rotation);
        return obj.gameObject;
    }

    private void ReturnToPool(VFXObject obj)
    {
        obj.gameObject.SetActive(false);

        foreach (var pool in vfxPools)
        {
            if (pool.prefab.name == obj.name.Replace("(Clone)", "").Trim())
            {
                pool.poolQueue.Enqueue(obj);
                return;
            }
        }

        Destroy(obj.gameObject); 
    }
}
