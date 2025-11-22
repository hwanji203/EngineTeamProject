using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraShaker : MonoSingleton<CameraShaker>
{
    [Header("Common")]
    [SerializeField] private float shakePower = 1f;

    [Header("Boss Stage Only")]
    [SerializeField] private bool isBossStage = false;       // 보스 스테이지 여부
    [SerializeField] private float defaultShakeDuration = 0.2f; // 보스 스테이지에서 흔들리는 시간
    [SerializeField] private Transform cameraRoot;              // 원래 위치로 돌릴 기준 (vcam 또는 부모)

    private CinemachineImpulseSource impulseSource;

    // 보스 스테이지용 - 기준 위치/회전
    private Vector3 originalLocalPos;
    private Quaternion originalLocalRot;
    private Coroutine resetCoroutine;

    protected override void Awake()
    {
        base.Awake();

        impulseSource = GetComponent<CinemachineImpulseSource>();

        // 보스 스테이지용 기준 트랜스폼 설정
        if (cameraRoot == null)
            cameraRoot = transform;

        originalLocalPos = cameraRoot.localPosition;
        originalLocalRot = cameraRoot.localRotation;
    }

    private void Start()
    {
        GameManager.Instance.Player.OnDamage += DirShake;
    }

    // 랜덤 방향 쉐이크
    public void RandomShake(float power = 0f)
    {
        if (!isBossStage)
        {
            // 기존 Hwan 코드 그대로
            impulseSource.GenerateImpulse(Random.insideUnitCircle.normalized * power * shakePower);
            return;
        }

        // 보스 스테이지용 (자동 복구 + duration)
        if (power <= 0f)
            power = 1f;

        impulseSource.GenerateImpulse(Random.insideUnitCircle.normalized * power * shakePower);
        StartResetTimer(defaultShakeDuration);
    }

    // 방향 있는 쉐이크 (OnDamage에서 호출)
    public void DirShake(float power, Vector2 dir)
    {
        if (!isBossStage)
        {
            // 기존 Hwan 코드 그대로
            impulseSource.GenerateImpulse(dir * shakePower * power);
            return;
        }

        // 보스 스테이지용
        impulseSource.GenerateImpulse(dir.normalized * shakePower * power);
        StartResetTimer(defaultShakeDuration);
    }

    // ---- 보스 스테이지용 복구 로직 ---- //

    private void StartResetTimer(float delay)
    {
        if (resetCoroutine != null)
            StopCoroutine(resetCoroutine);

        resetCoroutine = StartCoroutine(ResetAfter(delay));
    }

    private IEnumerator ResetAfter(float delay)
    {
        yield return new WaitForSeconds(delay);

        ResetShakeState();
        resetCoroutine = null;
    }

    // 실제로 "흔들림 종료 + 원래 위치 복구" 하는 메서드
    private void ResetShakeState()
    {
        // 1) 임펄스 신호 강제 초기화
        CinemachineImpulseManager.Instance.Clear();

        // 2) 카메라 기준 트랜스폼을 원래 로컬 위치/회전으로 복구
        if (cameraRoot == null)
            return;

        cameraRoot.localPosition = originalLocalPos;
        cameraRoot.localRotation = originalLocalRot;
    }

    // 외부에서 강제로 리셋하고 싶을 때
    public void SetPositionZero()
    {
        if (!isBossStage)
        {
            // 기존 버전 유지하고 싶으면 이대로 둬도 되고,
            // 필요하면 ResetShakeState()로 바꿔도 됨
            CinemachineImpulseManager.Instance.Clear();
            return;
        }

        ResetShakeState();
    }
}
