using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public enum EAudioMixerType { Master, BGM, SFX }

public class SoundManager : MonoSingleton<SoundManager>
{
    

    public enum BGM
    {
        test
    }

    public enum SFX
    {

    }

    [SerializeField]AudioClip[] bgmClips;
    [SerializeField]AudioClip[] sfxClips;


    [SerializeField]private AudioSource bgmAS;
    [SerializeField]private AudioSource sfxAS;

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }


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
