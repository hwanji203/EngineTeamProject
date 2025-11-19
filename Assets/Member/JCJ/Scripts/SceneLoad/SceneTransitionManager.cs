using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using TMPro;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    
    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    
    [Header("Loading Settings")]
    [SerializeField] private TextMeshProUGUI loadingText;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // 시작할 때 페이드 인
        fadeCanvasGroup.alpha = 1f;
        fadeCanvasGroup.DOFade(0f, fadeDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => fadeCanvasGroup.blocksRaycasts = false);
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        
        // Fade Out
        yield return fadeCanvasGroup.DOFade(1f, fadeDuration)
            .SetEase(Ease.InOutQuad)
            .WaitForCompletion();
        
        if (loadingText != null)
        {
            loadingText.gameObject.SetActive(true);
        }
        
        // 비동기 씬 로드
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        
        // 로딩 진행률 표시
        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            if (loadingText != null)
            {
                loadingText.text = $"Loading... {(progress * 100):F0}%";
            }
            
            yield return null;
        }
        
        // 씬 활성화
        operation.allowSceneActivation = true;
        
        // 씬이 완전히 로드될 때까지 대기
        yield return new WaitUntil(() => operation.isDone);
        
        // 로딩바 숨기기
        if (loadingText != null)
        {
            loadingText.gameObject.SetActive(false);
        }
        
        // Fade In
        yield return fadeCanvasGroup.DOFade(0f, fadeDuration)
            .SetEase(Ease.InOutQuad)
            .WaitForCompletion();
        
        fadeCanvasGroup.blocksRaycasts = false;
    }
}
