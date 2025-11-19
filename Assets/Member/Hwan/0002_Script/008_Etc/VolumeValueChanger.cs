using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeValueChanger: MonoBehaviour
{
    private Volume myVolume;
    private Action OnChangeEnd;

    private void Awake()
    {
        myVolume = GetComponent<Volume>();
    }

    public IEnumerator IncreaseWeight(float time)
    {
        myVolume.weight = 0f;

        while (myVolume.weight < 1f)
        {
            myVolume.weight += Time.unscaledDeltaTime / time;
            yield return null;
        }
        myVolume.weight = 1f;
        OnChangeEnd?.Invoke();
    }

    public IEnumerator DecreaseWeight(float time)
    {
        myVolume.weight = 1f;

        while (myVolume.weight > 0)
        {
            myVolume.weight -= Time.unscaledDeltaTime / time;
            yield return null;
        }
        myVolume.weight = 0f;
        OnChangeEnd?.Invoke();
    }

    public IEnumerator DecAfterInc(float time)
    {
        myVolume.weight = 0f;
        while (myVolume.weight < 1f)
        {
            myVolume.weight += Time.unscaledDeltaTime / (time / 3);
            yield return null;
        }

        myVolume.weight = 1f;
        while (myVolume.weight > 0)
        {
            myVolume.weight -= Time.unscaledDeltaTime / (time / 3 * 2);
            yield return null;
        }
        myVolume.weight = 0f;

        OnChangeEnd?.Invoke();
    }

    public void SetWeight(float value)
    {
        myVolume.weight = value;
    }
}
