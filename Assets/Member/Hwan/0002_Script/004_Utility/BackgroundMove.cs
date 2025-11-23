using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] [Range(0, 30)] private float speed;
    [SerializeField] private float errorRange = 0.05f;
    [SerializeField] private float maxRange = 10;
    [SerializeField] private float oneBlock = 1;
    [SerializeField] private bool doTeleport = true;
    [SerializeField] private bool isBackground = false;
    private Transform camTrn;

    private float lastPrameCamYValue;
    private float movedCamYValue;
    private Vector3 offset;


    private void Start()
    {
        camTrn = Camera.main.transform;
        if (isBackground == true)
        {
            transform.localPosition -= new Vector3(0, GameManager.Instance.StageSO.StartY);
        }
        offset = transform.localPosition;
        lastPrameCamYValue = camTrn.position.y;
    }

    private void Update()
    {
        movedCamYValue += (camTrn.position.y - lastPrameCamYValue) * 0.1f * speed;
        lastPrameCamYValue = camTrn.position.y;
        if (doTeleport == false)
        {
            transform.localPosition = new Vector3(0, -movedCamYValue) + offset;
            return;
        }

        if (Mathf.Abs(movedCamYValue) >= oneBlock - errorRange && movedCamYValue % 1 < errorRange)
        {
            transform.localPosition = offset;
            movedCamYValue = 0;
        }
        else if (movedCamYValue > maxRange)
        {
            transform.localPosition = offset;
        }
        else
        {
            transform.localPosition = new Vector3(0, -movedCamYValue) + offset;
        }
    }
}
