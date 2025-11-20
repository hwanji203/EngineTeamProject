using UnityEngine;

public class WrapObject : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        objectWidth = sr.bounds.size.x / 2;
        objectHeight = sr.bounds.size.y / 2;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x < -screenBounds.x - objectWidth)
            pos.x = screenBounds.x + objectWidth;

        else if (pos.x > screenBounds.x + objectWidth)
            pos.x = -screenBounds.x - objectWidth;

        if (pos.y < -screenBounds.y - objectHeight)
            pos.y = screenBounds.y + objectHeight;

        else if (pos.y > screenBounds.y + objectHeight)
            pos.y = -screenBounds.y - objectHeight;

        transform.position = pos;

        transform.position += Vector3.left * 2 * Time.deltaTime;

    }
}
