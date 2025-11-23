using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss
{
    public class BossManager : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.Instance.Play(BGMSoundType.BossStage3);
        }
    }
}
