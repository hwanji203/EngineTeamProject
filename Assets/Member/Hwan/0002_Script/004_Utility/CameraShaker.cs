using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShaker : MonoSingleton<CameraShaker>
{
    [SerializeField] private float shakePower;
    private CinemachineImpulseSource impulseSource;

    protected override void Awake()
    {
        base.Awake();

        impulseSource = GetComponent<CinemachineImpulseSource>(); 
    }

    private void Start()
    {
        GameManager.Instance.Player.OnDamage += DirShake;
    }

    public void RandomShake(float power = 0)
    {
        impulseSource.GenerateImpulse(Random.insideUnitCircle.normalized * power * shakePower);
    }

    public void DirShake(float power, Vector2 dir)
    {
        impulseSource.GenerateImpulse(dir * shakePower * power );
    }

    public void SetPositionZero()
    {
        CinemachineImpulseManager.Instance.Clear();
    }
}
