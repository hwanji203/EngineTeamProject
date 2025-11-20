using System;
using UnityEngine;

public class GroundChecker
{
    private Transform camTrn;
    private Transform playerTrn;

    public event Action OnNearGround;
    public NotifyValue<float> PlayerYPos = new NotifyValue<float>();

    public void Initialize(Transform camTrn, Transform playerTrn)
    {
        this.camTrn = camTrn;
        this.playerTrn = playerTrn;
    }

    private void CheckGround() { }
}
