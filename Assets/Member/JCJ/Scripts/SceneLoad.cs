using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public void Load()
    {
        SceneTransitionManager.Instance.LoadScene("JCJ");
    }
}
