using UnityEngine;

public class SeaUP : MonoBehaviour
{
    [SerializeField]private float defaultSpeed = 0.5f;

    private void Update()
    {
        transform.position += Vector3.up * defaultSpeed * Time.deltaTime;
    }


}
