using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

// 엔딩 크레딧을 위한 Unity UI 활용 예시
public class SquidCreditEnding : MonoBehaviour
{
    public Transform squid;                 // 오징어 Sprite
    public Transform background;            // 바다 배경
    public float startY = -7f;              // 오징어 시작 y
    public float surfaceY = 6.5f;           // 목표 y (수면)
    public float climbDuration = 6f;        // 오르기 시간

    public CanvasGroup creditPanel;         // 검은 패널(EndingCreditPanel)
    public RectTransform creditText;        // Text 또는 TMPro(TextMeshProUGUI)의 RectTransform
    public float creditFadeTime = 1.8f;     // 페이드 시간
    public float creditScrollTime = 18f;    // 크레딧 올라가는 시간

    [TextArea(10,25)]
    public string endingText = @"다리가 5개뿐인 외로운 오징어는
끝이 보이지 않던 어둠을 헤치고
마침내 반짝이는 해수면에 도달했다.

눈을 뜨자, 눈부신 빛 사이로
형형색색의 물고기들이 다가왔다.
파란 물고기, 주황 물고기, 노란 물고기가
물결 사이를 헤엄치며 말했다.

“안녕? 여기까지 오느라 많이 힘들었지?”

“혼자였지? 이제는 같이 놀자!”

오징어는 조금 머뭇거리다, 조심스럽게 대답했다.
“응… 나, 항상 혼자인 줄만 알았어.”

그러자 물고기들이 웃으며 주위를 빙글빙글 돌았다.
“그럼 오늘부터 우리는 친구야.”

“다리가 몇 개든 상관없어. 같이 헤엄치면 되잖아!”

오징어는 가슴이 따뜻해지는 것을 느꼈다.
깊은 바다에서 끝없이 올라오던 길은
사실, 혼자가 되기 위한 길이 아니라
친구들을 만나기 위한 여정이었다는 걸
이제서야 깨달았다.

그날 이후로, 다리가 5개뿐인 작은 오징어는
해수면 근처의 맑은 바다에서
새로 만난 물고기 친구들과 함께 헤엄치며 지냈다.

때로는 장난도 치고,
때로는 깊은 바다까지 모험을 떠나고,
서로를 향해 “내일 또 보자!”라고 인사하며
매일을 웃으며 보냈다.

더 이상 외톨이가 아니게 된 오징어는
그렇게, 새로운 친구들과 함께
오래오래 행복하게 살았습니다.";

    void Start()
    {
        // 시작 위치 초기화
        squid.localPosition = new Vector3(0, startY, 0);
        background.localPosition = Vector3.zero;

        // 크레딧 패널/텍스트 비활성화
        creditPanel.alpha = 0f;
        creditPanel.gameObject.SetActive(false);

        // 엔딩 연출 시작!
        StartCoroutine(PlayEndingCreditSequence());
    }

    IEnumerator PlayEndingCreditSequence()
    {
        // 1. 오징어가 천천히 위로 올라감
        yield return squid.DOLocalMoveY(surfaceY, climbDuration).SetEase(Ease.InOutSine).WaitForCompletion();
        yield return new WaitForSeconds(0.4f);

        // 2. 검은 패널 페이드 인 + 크레딧 텍스트 등장
        creditPanel.gameObject.SetActive(true);
        yield return creditPanel.DOFade(1f, creditFadeTime).WaitForCompletion();

        // 3. 텍스트를 위로 천천히 이동 (크레딧 올라가는 효과)
        // 크레딧 텍스트의 시작/끝 위치 설정
        var startPos = new Vector2(0, -Screen.height / 2);
        var endPos = new Vector2(0, Screen.height / 2 + creditText.rect.height);

        creditText.anchoredPosition = startPos;
        creditText.GetComponent<TextMeshProUGUI>().text = endingText; // 또는 TextMeshProUGUI

        yield return creditText.DOAnchorPos(endPos, creditScrollTime).SetEase(Ease.Linear).WaitForCompletion();

        // (Optional) 추가 버튼 노출/씬 전환 등
        // ...
    }
}
