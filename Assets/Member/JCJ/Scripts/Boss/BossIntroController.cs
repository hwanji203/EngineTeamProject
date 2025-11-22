using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class BossIntroController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup bossIntroOverlay;
    [SerializeField] private Image darkBackground;
    [SerializeField] private Image greenBanner;
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private Image bossCharacter;
    [SerializeField] private Image warningLine1;
    [SerializeField] private Image warningLine2;
    [SerializeField] private ParticleSystem particleSystem;

    [Header("Boss Info")]
    [SerializeField] private string bossName = "DEVIL'S SNARE";
    [SerializeField] private Sprite bossSprite;

    [Header("Animation Settings")]
    [SerializeField] private float bannerSlideInDuration = 0.8f;
    [SerializeField] private float characterAppearDuration = 1.0f;
    [SerializeField] private float totalIntroDuration = 4.0f;

    [Header("Position Settings")]
    [SerializeField] private float bossCharacterTargetX = -350f; // 보스 최종 X 위치
    [SerializeField] private float bossCharacterTargetY = 0f;    // 보스 최종 Y 위치
    [SerializeField] private float bossNameTextTargetX = 400f;   // 텍스트 최종 X 위치
    [SerializeField] private float bossNameTextTargetY = 0f;     // 텍스트 최종 Y 위치

    [Header("Camera Shake")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float shakeIntensity = 0.5f;
    [SerializeField] private float shakeDuration = 0.3f;

    private Vector3 originalCameraPosition;
    private Sequence bossIntroSequence;

    // 초기 위치 저장용
    private Vector2 originalTextPosition;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        
        originalCameraPosition = mainCamera.transform.position;

        // 보스 스프라이트 설정
        if (bossSprite != null)
            bossCharacter.sprite = bossSprite;

        // 텍스트 초기 위치 저장
        originalTextPosition = bossNameText.rectTransform.anchoredPosition;

        // 초기 상태 설정
        ResetIntro();
    }

    private void Start()
    {
        // 테스트용 자동 재생
        StartCoroutine(TestPlayAfterDelay(1f));
    }

    private IEnumerator TestPlayAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TriggerBossIntro();
    }

    // 외부에서 호출할 보스 등장
    public void TriggerBossIntro()
    {
        StopAllCoroutines();
        ResetIntro();
        PlayBossIntro();
    }

    private void ResetIntro()
    {
        // 모든 요소를 초기 상태로
        bossIntroOverlay.alpha = 1;
        bossIntroOverlay.gameObject.SetActive(false);

        darkBackground.color = new Color(0, 0, 0, 0);
        
        // 배너를 화면 왼쪽 밖으로
        greenBanner.rectTransform.anchoredPosition = new Vector2(-2500, 0);
        
        // 텍스트 초기화 (화면 밖으로)
        bossNameText.text = "";
        bossNameText.alpha = 0;
        bossNameText.rectTransform.anchoredPosition = new Vector2(-2000, bossNameTextTargetY);

        // 보스 캐릭터를 화면 오른쪽 밖으로
        bossCharacter.rectTransform.anchoredPosition = new Vector2(2000, bossCharacterTargetY);
        bossCharacter.color = new Color(1, 1, 1, 0);
        bossCharacter.transform.localScale = Vector3.one * 0.5f;
        bossCharacter.transform.rotation = Quaternion.Euler(0, 0, -15f);

        // 경고선 초기화
        warningLine1.color = new Color(1, 0.27f, 0.27f, 0);
        warningLine2.color = new Color(1, 0.27f, 0.27f, 0);

        // 파티클 정지
        particleSystem.Stop();
        particleSystem.Clear();
    }

    private void PlayBossIntro()
    {
        bossIntroOverlay.gameObject.SetActive(true);

        bossIntroSequence = DOTween.Sequence();

        //화면 어둡게 + 플래시 효과
        bossIntroSequence.Append(darkBackground.DOColor(new Color(0, 0, 0, 0.7f), 0.3f).SetEase(Ease.OutCubic));
        bossIntroSequence.Join(ScreenFlash());

        //경고선 등장 (0.2초 후)
        bossIntroSequence.AppendInterval(0.2f);
        bossIntroSequence.Append(warningLine1.DOColor(new Color(1, 0.27f, 0.27f, 0.8f), 0.2f));
        bossIntroSequence.Join(warningLine2.DOColor(new Color(1, 0.27f, 0.27f, 0.8f), 0.2f));

        //그린 배너 슬라이드 인 (0.3초 후)
        bossIntroSequence.AppendInterval(0.3f);
        bossIntroSequence.Append(greenBanner.rectTransform.DOAnchorPosX(0, bannerSlideInDuration).SetEase(Ease.OutExpo));

        //보스 이름 타이핑 효과 (0.2초 후) - Position Settings 변수 사용!
        bossIntroSequence.AppendInterval(0.2f);
        bossIntroSequence.Append(bossNameText.rectTransform.DOAnchorPos(
            new Vector2(bossNameTextTargetX, bossNameTextTargetY), 
            0.5f).SetEase(Ease.OutExpo));
        bossIntroSequence.Join(bossNameText.DOFade(1, 0.3f));
        bossIntroSequence.Join(TypewriterEffect());

        //보스 캐릭터 등장 + 카메라 셰이크 (0.3초 후) - Position Settings 변수 사용!
        bossIntroSequence.AppendInterval(0.3f);
        bossIntroSequence.Append(bossCharacter.rectTransform.DOAnchorPos(
            new Vector2(bossCharacterTargetX, bossCharacterTargetY), 
            characterAppearDuration).SetEase(Ease.OutBack));
        bossIntroSequence.Join(bossCharacter.DOColor(Color.white, 0.5f));
        bossIntroSequence.Join(bossCharacter.transform.DOScale(Vector3.one, characterAppearDuration).SetEase(Ease.OutBack));
        bossIntroSequence.Join(bossCharacter.transform.DORotate(Vector3.zero, characterAppearDuration).SetEase(Ease.OutBack));
        bossIntroSequence.AppendCallback(() => CameraShake());

        //글로우 효과 + 파티클 (0.2초 후)
        bossIntroSequence.AppendInterval(0.2f);
        bossIntroSequence.AppendCallback(() => particleSystem.Play());

        //1초 동안 유지
        bossIntroSequence.AppendInterval(1.0f);

        //페이드 아웃 (모든 요소)
        bossIntroSequence.Append(greenBanner.rectTransform.DOAnchorPosX(2500, 0.8f).SetEase(Ease.InExpo));
        bossIntroSequence.Join(bossCharacter.rectTransform.DOAnchorPosX(2000, 0.8f).SetEase(Ease.InExpo));
        bossIntroSequence.Join(bossNameText.rectTransform.DOAnchorPosX(2000, 0.8f).SetEase(Ease.InExpo));
        bossIntroSequence.Join(bossNameText.DOFade(0, 0.5f));
        bossIntroSequence.Join(warningLine1.DOColor(new Color(1, 0.27f, 0.27f, 0), 0.5f));
        bossIntroSequence.Join(warningLine2.DOColor(new Color(1, 0.27f, 0.27f, 0), 0.5f));
        bossIntroSequence.Join(darkBackground.DOColor(new Color(0, 0, 0, 0), 0.8f));

        //완료 후 비활성화
        bossIntroSequence.OnComplete(() =>
        {
            particleSystem.Stop();
            bossIntroOverlay.gameObject.SetActive(false);
        });
    }

    private Tween ScreenFlash()
    {
        Image flashImage = darkBackground;
        
        return DOTween.Sequence()
            .Append(flashImage.DOColor(new Color(1, 1, 1, 0.5f), 0.1f))
            .Append(flashImage.DOColor(new Color(0, 0, 0, 0.7f), 0.2f));
    }

    private Tween TypewriterEffect()
    {
        bossNameText.maxVisibleCharacters = 0;
        bossNameText.text = bossName;
        
        return DOTween.To(() => bossNameText.maxVisibleCharacters,
            x => bossNameText.maxVisibleCharacters = x,
            bossName.Length,
            0.8f).SetEase(Ease.Linear);
    }

    private void CameraShake()
    {
        mainCamera.transform.DOShakePosition(shakeDuration, shakeIntensity, 20, 90, false, true)
            .OnComplete(() => mainCamera.transform.position = originalCameraPosition);
    }

    private void OnDestroy()
    {
        // DOTween 시퀀스 정리
        bossIntroSequence?.Kill();
    }
}
