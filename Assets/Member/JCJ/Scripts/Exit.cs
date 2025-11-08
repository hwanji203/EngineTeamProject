
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Exited(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
