using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SquidCreditEnding : MonoBehaviour
{
    [SerializeField] private Transform squid;
    [SerializeField] private Transform background;
    [SerializeField] private float startY = -7f;
    [SerializeField] private float surfaceY = 6.5f;
    [SerializeField] private float climbDuration = 6f;

    [SerializeField] private Image endingImage;
    [SerializeField] private CanvasGroup creditPanel;
    [SerializeField] private RectTransform creditText;
    [SerializeField] private float imageFadeTime = 1.5f;
    [SerializeField] private float creditFadeTime = 1.8f;
    [SerializeField] private float textFadeTime = 1.2f;
    [SerializeField] private float creditScrollTime = 18f;

    [TextArea(10,25)]
    private string endingText = 
        @"지상으로 올라온 오징어는
낚시꾼들에게 붙잡혀
오징어 구이가 되어버렸다고 합니다.



개발 : 김용민,지태환,유현우,전창준
기획 : 김용민,지태환

Thank you for playing.";

    private TextMeshProUGUI creditTextComponent;

    void Start()
    {
        squid.localPosition = new Vector3(0, startY, 0);
        background.localPosition = Vector3.zero;
        if (endingImage != null)
        {
            Color imgColor = endingImage.color;
            imgColor.a = 0f;
            endingImage.color = imgColor;
        }

        creditPanel.alpha = 0f;
        creditPanel.gameObject.SetActive(false);

        creditTextComponent = creditText.GetComponent<TextMeshProUGUI>();

        StartCoroutine(PlayEndingCreditSequence());
    }

    IEnumerator PlayEndingCreditSequence()
    {
        yield return squid.DOLocalMoveY(surfaceY, climbDuration).SetEase(Ease.InOutSine).WaitForCompletion();
        yield return new WaitForSeconds(0.4f);
        creditPanel.gameObject.SetActive(true);
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.Append(creditPanel.DOFade(1f, creditFadeTime));
        
        if (endingImage != null)
        {
            fadeSequence.Join(endingImage.DOFade(1f, imageFadeTime));
        }
        
        yield return fadeSequence.WaitForCompletion();
        float currentX = creditText.anchoredPosition.x;
        var startPos = new Vector2(currentX, -Screen.height / 2);
        var endPos = new Vector2(currentX, Screen.height / 2 + creditText.rect.height);

        creditText.anchoredPosition = startPos;
        creditTextComponent.text = endingText;
        Color textColor = creditTextComponent.color;
        textColor.a = 0f;
        creditTextComponent.color = textColor;
        yield return creditTextComponent.DOFade(1f, textFadeTime).WaitForCompletion();
        yield return creditText.DOAnchorPos(endPos, creditScrollTime).SetEase(Ease.Linear).WaitForCompletion();
    }
}
