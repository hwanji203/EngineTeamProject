using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 1080f; // �ſ� ������
    [SerializeField] private float duration = 0.2f;   // ȸ�� �ð�

    private float timer;

    void Update()
    {
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= duration)
            Destroy(gameObject); // ����Ʈ �����
    }
}
