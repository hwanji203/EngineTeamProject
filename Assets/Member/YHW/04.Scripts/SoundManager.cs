using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    

    public enum BGM
    {
        
    }

    public enum SFX
    {

    }

    [SerializeField]AudioClip[] bgmClips;
    [SerializeField]AudioClip[] sfxClips;


    [SerializeField]private AudioSource bgmAS;
    [SerializeField]private AudioSource sfxAS;


   

    public void PlayBgm(BGM bgmidx)
    {
        bgmAS.clip = bgmClips[(int)bgmidx];
        bgmAS.Play();
    }

    public void PlaySfx(SFX sfxidx)
    {
        sfxAS.PlayOneShot(sfxClips[(int)sfxidx]);
    }
    public void StopBgm()
    {
        bgmAS.Stop();   
    }
}
