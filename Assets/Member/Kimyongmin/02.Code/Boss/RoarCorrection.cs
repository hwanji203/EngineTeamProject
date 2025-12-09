using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss
{
    public class RoarCorrection : MonoBehaviour
    {
        private PointEffector2D _pointEffector;

        private void Awake()
        {
            _pointEffector = GetComponent<PointEffector2D>();
        }
    }
}
