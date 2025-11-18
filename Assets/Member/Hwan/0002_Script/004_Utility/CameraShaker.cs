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

    public void RandomShake(float power = 0)
    {
        if (power == 0) power = shakePower;

        impulseSource.GenerateImpulse(Random.insideUnitCircle.normalized * power);
    }

    public void DirShake(float power, Vector2 dir)
    {
        if (power == 0) power = shakePower;

        impulseSource.GenerateImpulse(dir * shakePower);
    }

    public void SetPositionZero()
    {
        CinemachineImpulseManager.Instance.Clear();
    }
}
