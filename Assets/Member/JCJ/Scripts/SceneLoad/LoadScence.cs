using UnityEngine;

public class LoadScence : MonoBehaviour
{
    public void Load()
    {
        SceneTransitionManager.Instance.LoadScene("SceneScreen");
    }
}
