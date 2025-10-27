using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

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


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
