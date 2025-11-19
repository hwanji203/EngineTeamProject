using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class VolumeManager : MonoSingleton<VolumeManager>
{
    [SerializeField] private Volumes[] volumes;
    private Dictionary<VolumeType, VolumeValueChanger> volumeDictionary = new();

    protected override void Awake()
    {
        base.Awake();

        foreach (Volumes volume in volumes)
        {
            volumeDictionary.Add(volume.VolumeType, volume.VolumeChanger);
        }
    }

    private void Start()
    {
        volumeDictionary[VolumeType.Normal].SetWeight(1);

        Player player = GameManager.Instance.Player;
        player.OnDamage += (_, _) => DefAfterInc(VolumeType.Hit, 0.35f);
        SubAction(VolumeType.NoAir, ref player.StaminaCompo.OnNoAir);
    }

    public void IncreaseVolume(VolumeType type, float time)
    {
        StartCoroutine(volumeDictionary[type].IncreaseWeight(time));
    }

    public void DecreaseVolume(VolumeType type, float time)
    {
        StartCoroutine(volumeDictionary[type].DecreaseWeight(time));
    }

    public void DefAfterInc(VolumeType type, float time)
    {
        StartCoroutine(volumeDictionary[type].DecAfterInc(time));   
    }

    public void SubAction(VolumeType type, ref Action<float> action)
    {
        action += volumeDictionary[type].SetWeight;
    }

    public void UnSubAction(VolumeType type, ref Action<float> action)
    {
        action -= volumeDictionary[type].SetWeight;
    }
}

[Serializable]
public class Volumes
{
    [field: SerializeField] public VolumeType VolumeType { get; private set; }
    [field: SerializeField] public VolumeValueChanger VolumeChanger { get; private set; } 
}
