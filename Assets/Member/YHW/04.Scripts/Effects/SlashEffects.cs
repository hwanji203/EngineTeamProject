using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 1080f; // 매우 빠르게
    [SerializeField] private float duration = 0.2f;   // 회전 시간

    private float timer;

    void Update()
    {
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= duration)
            Destroy(gameObject); // 이펙트 사라짐
    }
}
