using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public enum SFXSoundType
{
    None,
    OnBattle,
    EnemyDashDamaged,
    GenEnemy,
    Spin,
    Clear,
    GetDamage,
    GetHeal,
    Typing,
    GameOver,
    Counter,
    Dash,
    ButtonMouseIn,
    EnemyFlipDamaged,
    SharkRoar,
    SharkRoarShorts,
    SharkLaserPointer,
    SharkLaser,
    SharkBite,
    SharkSpin,
    SharkBoom,
    Slap
}

public enum BGMSoundType
{
    None,
    MainScene,
    Tutorial,
    Stage2,
    BossStage3,
    Stage4,
    Stage5,
    Stage6,
    Intro
}
public class SoundManager : MonoSingleton<SoundManager>
{
    [Serializable]
    public class ClipData <T> where T : Enum
    {
        [field: SerializeField] public T Type { get; private set; }     // 사운드 구분 타입
        [field: SerializeField] public AudioClip Clip { get; private set; }      // 오디오 파일
    }

    [Serializable]
    public class AudioData<T> where T : Enum
    {
        [field: SerializeField] public ClipData<T>[] ClipDatas { get; private set; }
        [field: SerializeField] public AudioMixerGroup Mixer { get; private set; }
    }

    [Header("SFX Sound Daters")]
    [SerializeField] private AudioData<SFXSoundType> sfxSounds;

    [Header("BGM Sound Daters")]
    [SerializeField] private AudioData<BGMSoundType> bgmSounds;

    private Dictionary<SFXSoundType, AudioClip> sfxSoundDict;
    private AudioSource sfxAudioSource;

    private Dictionary<BGMSoundType, AudioClip> bgmSoundDict;
    private AudioSource bgmAudioSource;
    
    protected override void Awake()
    {
        base.Awake();

        AudioInitialize<SFXSoundType>(out sfxAudioSource, out sfxSoundDict, sfxSounds);
        AudioInitialize<BGMSoundType>(out bgmAudioSource, out bgmSoundDict, bgmSounds);

        bgmAudioSource.loop = true;
    }

    private void AudioInitialize<T>(out AudioSource audioSource, out Dictionary<T, AudioClip> dict, AudioData<T> soundData) where T : Enum
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = soundData.Mixer;

        dict = new Dictionary<T, AudioClip>();
        foreach (ClipData<T> clipData in soundData.ClipDatas)
        {
            if (dict.ContainsKey(clipData.Type) == false)
            {
                dict.Add(clipData.Type, clipData.Clip);
            }
        }
    }

    // 지정된 타입의 사운드 재생
    public void Play(SFXSoundType type)
    {
        if (sfxSoundDict.TryGetValue(type, out var clip))
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
    public void Play(BGMSoundType type, float fadeTime = 0f)
    {
        if (bgmSoundDict.TryGetValue(type, out var clip))
        {
            StartCoroutine(FadeBGM(clip, fadeTime));
        }
    }

    private IEnumerator FadeBGM(AudioClip newClip, float fadeTime)
    {
        if (bgmAudioSource.isPlaying)
        {
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                bgmAudioSource.volume = 1 - (t / fadeTime);
                yield return null;
            }
        }

        bgmAudioSource.clip = newClip;
        bgmAudioSource.Play();

        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            bgmAudioSource.volume = t / fadeTime;
            yield return null;
        }
        bgmAudioSource.volume = 1f;
    }
}
