using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 2f;
    [SerializeField]float backgroundWidth;

    private Transform t;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        backgroundWidth = sr.bounds.size.x;
        t = transform;
    }

    private void Update()
    {
        t.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (t.position.x <= -backgroundWidth)
        {
            t.position += new Vector3(backgroundWidth * 2f, 0, 0);
        }
    }
}
