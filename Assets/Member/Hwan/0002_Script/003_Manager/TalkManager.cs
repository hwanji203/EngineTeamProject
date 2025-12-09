using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TalkManager : MonoSingleton<TalkManager>
{
    [SerializeField] private float typingSpeed = 0.01f; // 글자당 출력 속도
    //[SerializeField] private GameObject textBar;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public event Action OnTypingEnd;

    private Coroutine typingCoroutine;

    private bool isFloatingChars = false;
    private Mesh mesh;
    private Vector3[] vertices;
    private string currentMessage;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        // ★ 1) 클릭으로 타이핑 스킵

        // ★ 2) 이하 기존 흔들기 코드
        if (!isFloatingChars) return;
        if (textMeshPro.textInfo == null) return;

        textMeshPro.ForceMeshUpdate();
        var textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int vertexIndex = charInfo.vertexIndex;
            int meshIndex = charInfo.materialReferenceIndex;

            mesh = textInfo.meshInfo[meshIndex].mesh;
            vertices = mesh.vertices;

            float offset = Mathf.Sin(Time.unscaledTime * 3f + i * 0.3f);
            float yMove = offset * 5f;

            Vector3 move = new Vector3(0, yMove, 0);
            vertices[vertexIndex + 0] += move;
            vertices[vertexIndex + 1] += move;
            vertices[vertexIndex + 2] += move;
            vertices[vertexIndex + 3] += move;

            textInfo.meshInfo[meshIndex].mesh.vertices = vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[meshIndex].mesh, meshIndex);
        }
    }


    public void StartTyping(string message)
    {
        textMeshPro.gameObject.SetActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        currentMessage = message;   // ★ 여기서 저장

        StartFloating();  // ← 텍스트 표시 동안 위아래로 둥실둥실
        typingCoroutine = StartCoroutine(TypeText(message));
    }


    private IEnumerator TypeText(string message)
    {
        textMeshPro.text = "";

        foreach (char c in message)
        {
            if (c == '\\')
            {
                textMeshPro.text += c;
                textMeshPro.text += 'n';
                continue;
            }
            else if (c == 'n')
            {
                continue;
            }


            textMeshPro.text += c;
            SoundManager.Instance.Play(SFXSoundType.Typing);
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(1.5f);

        typingCoroutine = null;
        OnTypingEnd?.Invoke();
    }



    public void CompleteTyping(string message)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        textMeshPro.text = message;
        OnTypingEnd?.Invoke();
    }
    public void CompleteTyping()
    {
        if (string.IsNullOrEmpty(currentMessage))
            return;

        CompleteTyping(currentMessage);
    }

    private void StartFloating()
    {
        isFloatingChars = true;
        textMeshPro.ForceMeshUpdate();
    }


    private void StopFloating()
    {
        try
        {
            textMeshPro.ForceMeshUpdate();
            mesh = textMeshPro.mesh;
            vertices = mesh.vertices;

            isFloatingChars = false;

            for (int i = 0; i < vertices.Length; i++)
                vertices[i] = Vector3.zero;

            textMeshPro.canvasRenderer.SetMesh(mesh);
        }
        catch
        {
            return;
        }
    }

    public void ActiveFalse()
    {
        // 흔들리던 것 멈춤
        StopFloating();

        textMeshPro.gameObject.SetActive(true);

        // 텍스트 완전히 초기화
        textMeshPro.text = "";

        // 혹시 이전 타이핑 코루틴이 남아 있다면 정리
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

}
