using UnityEngine;
using System.Collections;

public class TimeManager : MonoSingleton<TimeManager>
{

    public float UIDeltaTime => Time.unscaledDeltaTime;
    public float UITime => Time.unscaledTime;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }



    public Coroutine StartUICoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public IEnumerator UIWait(float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
