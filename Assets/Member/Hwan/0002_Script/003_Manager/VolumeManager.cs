using System;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoSingleton<VolumeManager>
{
    [SerializeField] private Volumes[] volumes;
    private Dictionary<VolumeType, VolumeValueChanger> volumeDictionary = new();

    private Coroutine defAfterInCoroutine;

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
        foreach (VolumeValueChanger volume in volumeDictionary.Values)
        {
            volume.SetWeight(0);
        }
        volumeDictionary[VolumeType.Normal].SetWeight(1);

        Player player = GameManager.Instance.Player;
        player.OnDamage += (_, _) => DefAfterInc(VolumeType.Hit, 0.35f);
        player.StaminaCompo.SubOnNoAir((value) => SetVolume(VolumeType.NoAir, value));
        player.PositionCheckerCompo.SubNearGround((value) => SetVolume(VolumeType.EndOfCam, value));
        player.PositionCheckerCompo.SubNearClear((value) => SetVolume(VolumeType.EndOfClear, value));
        player.AttackCompo.OnAttack += (value) =>
        {
            if (value == true)
            {
                DefAfterInc(VolumeType.Counter, 0.2f);
            }
        };
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
        if (defAfterInCoroutine != null) StopCoroutine(defAfterInCoroutine);
        defAfterInCoroutine = StartCoroutine(volumeDictionary[type].DecAfterInc(time));   
    }

    public void SetVolume(VolumeType type, float value)
    {
        volumeDictionary[type].SetWeight(value);
    }
}

[Serializable]
public class Volumes
{
    [field: SerializeField] public VolumeType VolumeType { get; private set; }
    [field: SerializeField] public VolumeValueChanger VolumeChanger { get; private set; } 
}
