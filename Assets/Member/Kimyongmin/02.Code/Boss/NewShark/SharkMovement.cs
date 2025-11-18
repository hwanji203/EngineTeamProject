using System;
using UnityEngine;

[Serializable]
public class SharkMovement : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }

    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
    }
}
