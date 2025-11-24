using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class BossIntroController : MonoBehaviour
{
    public static BossIntroController Instance { get; private set; }

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
    [SerializeField] private string bossName = "Shark";
    [SerializeField] private Sprite bossSprite;

    [Header("Animation Settings")]
    [SerializeField] private float bannerSlideInDuration = 0.8f;
    [SerializeField] private float characterAppearDuration = 1.0f;
    [SerializeField] private float totalIntroDuration = 4.0f;

    [Header("Position Settings")]
    [SerializeField] private float bossCharacterTargetX = -350f;
    [SerializeField] private float bossCharacterTargetY = 0f;
    [SerializeField] private float bossNameTextTargetX = 400f;
    [SerializeField] private float bossNameTextTargetY = 0f;

    [Header("Camera Shake")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float shakeIntensity = 0.5f;
    [SerializeField] private float shakeDuration = 0.3f;

    [Header("Time Scale Settings")]
    [SerializeField] private bool pauseGameDuringIntro = true;

    [Header("Debug")]
    [SerializeField] private bool autoPlayOnStart = false;

    private Vector3 originalCameraPosition;
    private Sequence bossIntroSequence;
    private Vector2 originalTextPosition;
    private float originalTimeScale = 1f;
    private bool isIntroPlaying = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (mainCamera == null)
            mainCamera = Camera.main;
        
        if (mainCamera != null)
            originalCameraPosition = mainCamera.transform.position;

        if (bossSprite != null && bossCharacter != null)
            bossCharacter.sprite = bossSprite;

        if (bossNameText != null)
            originalTextPosition = bossNameText.rectTransform.anchoredPosition;

        ResetIntro();
    }

    private void Start()
    {
        StartCoroutine(TestPlayAfterDelay(0.1f)); }

    private IEnumerator TestPlayAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        TriggerBossIntro();
    }

    public void SetBossInfo(string name, Sprite sprite)
    {
        bossName = name;
        bossSprite = sprite;
        
        if (bossCharacter != null && sprite != null)
        {
            bossCharacter.sprite = sprite;
        }
    }

    public void TriggerBossIntro()
    {
        if (isIntroPlaying)
        {
            Debug.LogWarning("Boss intro is already playing!");
            return;
        }

        StopAllCoroutines();
        ResetIntro();
        PlayBossIntro();
    }

    private void ResetIntro()
    {
        if (bossIntroOverlay == null) return;

        bossIntroOverlay.alpha = 1;
        bossIntroOverlay.gameObject.SetActive(false);

        if (darkBackground != null)
            darkBackground.color = new Color(0, 0, 0, 0);
        
        if (greenBanner != null)
            greenBanner.rectTransform.anchoredPosition = new Vector2(-2500, 0);
        
        if (bossNameText != null)
        {
            bossNameText.text = "";
            bossNameText.alpha = 0;
            bossNameText.rectTransform.anchoredPosition = new Vector2(-2000, bossNameTextTargetY);
        }

        if (bossCharacter != null)
        {
            bossCharacter.rectTransform.anchoredPosition = new Vector2(2000, bossCharacterTargetY);
            bossCharacter.color = new Color(1, 1, 1, 0);
            bossCharacter.transform.localScale = Vector3.one * 0.5f;
            bossCharacter.transform.rotation = Quaternion.Euler(0, 0, -15f);
        }

        if (warningLine1 != null)
            warningLine1.color = new Color(1, 0.27f, 0.27f, 0);
        
        if (warningLine2 != null)
            warningLine2.color = new Color(1, 0.27f, 0.27f, 0);

        if (particleSystem != null)
        {
            particleSystem.Stop();
            particleSystem.Clear();
        }
    }

    private void PlayBossIntro()
    {
        if (bossIntroOverlay == null)
        {
            Debug.LogError("Boss intro overlay is not assigned!");
            return;
        }

        bossIntroOverlay.gameObject.SetActive(true);

        if (pauseGameDuringIntro)
        {
            originalTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isIntroPlaying = true;
        }

        bossIntroSequence = DOTween.Sequence();
        bossIntroSequence.SetUpdate(true);

        bossIntroSequence.Append(darkBackground.DOColor(new Color(0, 0, 0, 0.7f), 0.3f).SetEase(Ease.OutCubic));
        bossIntroSequence.Join(ScreenFlash());

        bossIntroSequence.AppendInterval(0.2f);
        bossIntroSequence.Append(warningLine1.DOColor(new Color(1, 0.27f, 0.27f, 0.8f), 0.2f));
        bossIntroSequence.Join(warningLine2.DOColor(new Color(1, 0.27f, 0.27f, 0.8f), 0.2f));

        bossIntroSequence.AppendInterval(0.3f);
        bossIntroSequence.Append(greenBanner.rectTransform.DOAnchorPosX(0, bannerSlideInDuration).SetEase(Ease.OutExpo));

        bossIntroSequence.AppendInterval(0.2f);
        bossIntroSequence.Append(bossNameText.rectTransform.DOAnchorPos(
            new Vector2(bossNameTextTargetX, bossNameTextTargetY), 
            0.5f).SetEase(Ease.OutExpo));
        bossIntroSequence.Join(bossNameText.DOFade(1, 0.3f));
        bossIntroSequence.Join(TypewriterEffect());

        bossIntroSequence.AppendInterval(0.3f);
        bossIntroSequence.Append(bossCharacter.rectTransform.DOAnchorPos(
            new Vector2(bossCharacterTargetX, bossCharacterTargetY), 
            characterAppearDuration).SetEase(Ease.OutBack));
        bossIntroSequence.Join(bossCharacter.DOColor(Color.white, 0.5f));
        bossIntroSequence.Join(bossCharacter.transform.DOScale(Vector3.one, characterAppearDuration).SetEase(Ease.OutBack));
        bossIntroSequence.Join(bossCharacter.transform.DORotate(Vector3.zero, characterAppearDuration).SetEase(Ease.OutBack));
        bossIntroSequence.AppendCallback(() => CameraShake());

        bossIntroSequence.AppendInterval(0.2f);
        bossIntroSequence.AppendCallback(() => {
            if (particleSystem != null)
                particleSystem.Play();
        });

        bossIntroSequence.AppendInterval(1.0f);

        bossIntroSequence.Append(greenBanner.rectTransform.DOAnchorPosX(2500, 0.8f).SetEase(Ease.InExpo));
        bossIntroSequence.Join(bossCharacter.rectTransform.DOAnchorPosX(2000, 0.8f).SetEase(Ease.InExpo));
        bossIntroSequence.Join(bossNameText.rectTransform.DOAnchorPosX(2000, 0.8f).SetEase(Ease.InExpo));
        bossIntroSequence.Join(bossNameText.DOFade(0, 0.5f));
        bossIntroSequence.Join(warningLine1.DOColor(new Color(1, 0.27f, 0.27f, 0), 0.5f));
        bossIntroSequence.Join(warningLine2.DOColor(new Color(1, 0.27f, 0.27f, 0), 0.5f));
        bossIntroSequence.Join(darkBackground.DOColor(new Color(0, 0, 0, 0), 0.8f));

        bossIntroSequence.OnComplete(() =>
        {
            if (particleSystem != null)
                particleSystem.Stop();
            
            bossIntroOverlay.gameObject.SetActive(false);
            
            if (pauseGameDuringIntro && isIntroPlaying)
            {
                Time.timeScale = originalTimeScale;
                isIntroPlaying = false;
            }
        });
    }

    private Tween ScreenFlash()
    {
        Image flashImage = darkBackground;
        
        return DOTween.Sequence()
            .Append(flashImage.DOColor(new Color(1, 1, 1, 0.5f), 0.1f))
            .Append(flashImage.DOColor(new Color(0, 0, 0, 0.7f), 0.2f))
            .SetUpdate(true);
    }

    private Tween TypewriterEffect()
    {
        bossNameText.maxVisibleCharacters = 0;
        bossNameText.text = bossName;
        
        return DOTween.To(() => bossNameText.maxVisibleCharacters,
            x => bossNameText.maxVisibleCharacters = x,
            bossName.Length,
            0.8f)
            .SetEase(Ease.Linear)
            .SetUpdate(true);
    }

    private void CameraShake()
    {
        if (mainCamera == null) return;

        mainCamera.transform.DOShakePosition(shakeDuration, shakeIntensity, 20, 90, false, true)
            .SetUpdate(true)
            .OnComplete(() => {
                if (mainCamera != null)
                    mainCamera.transform.position = originalCameraPosition;
            });
    }

    private void OnDestroy()
    {
        bossIntroSequence?.Kill();
        
        if (isIntroPlaying)
        {
            Time.timeScale = originalTimeScale;
            isIntroPlaying = false;
        }

        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        if (isIntroPlaying)
        {
            Time.timeScale = originalTimeScale;
        }
    }

    // Public API
    public bool IsPlaying => isIntroPlaying;

    public void StopIntro()
    {
        if (bossIntroSequence != null && bossIntroSequence.IsActive())
        {
            bossIntroSequence.Kill();
        }

        if (particleSystem != null)
            particleSystem.Stop();

        if (bossIntroOverlay != null)
            bossIntroOverlay.gameObject.SetActive(false);

        if (pauseGameDuringIntro && isIntroPlaying)
        {
            Time.timeScale = originalTimeScale;
            isIntroPlaying = false;
        }
    }
}
