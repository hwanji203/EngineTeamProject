using UnityEngine;

public class ForAniEvents : MonoBehaviour
{
    public void FillStar()
    {
        StarManager.Instance.ChangeState();
    }
}
