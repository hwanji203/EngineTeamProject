using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public enum SFXSoundType
{
    None,
    OnBattle,
    EnemyGetDamage,
    GenEnemy,
    Spin,
    Clear,
    GetDamage,
    GetHeal,
    Typing,
    GameOver,
    Counter,
    Dash,
    ButtonMouseIn
}

public enum BGMSoundType
{
    None,
    MainScene,
    Tutorial,
    Stage2,
    Stage3,
    Stage4,
    Boss,
    Intro
}
public class SoundManager : MonoSingleton<SoundManager>
{
    [Serializable]
    public class ClipData <T>
    {
        [field: SerializeField] public T Type { get; private set; }     // 사운드 구분 타입
        [field: SerializeField] public AudioClip Clip { get; private set; }      // 오디오 파일
    }

    [Serializable]
    public class SoundData<T>
    {
        [field: SerializeField] public ClipData<T>[] ClipDatas { get; private set; }
        [field: SerializeField] public AudioMixerGroup Mixer { get; private set; }
    }

    [Header("SFX Sound Daters")]
    [SerializeField] private SoundData<SFXSoundType> sfxSounds;

    [Header("BGM Sound Daters")]
    [SerializeField] private SoundData<BGMSoundType> bgmSounds;

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

    private void AudioInitialize<T>(out AudioSource audioSource, out Dictionary<T, AudioClip> dict, SoundData<T> soundData)
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
