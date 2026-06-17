using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public enum TimeMode
    {
        Normal,
        Pause,
    }

    public static void SetTimeMode(TimeMode timeMode)
    {
        if (timeMode == TimeMode.Normal)
            Time.timeScale = 1.0f;
        else if(timeMode == TimeMode.Pause)
            Time.timeScale = 0.0f;
    }

    public static IEnumerator StartTimer(float time, Action method)
    {
        yield return new WaitForSeconds(time);

        method?.Invoke();
    }

    public static IEnumerator StartRealTimer(float time, Action method)
    {
        yield return new WaitForSecondsRealtime(time);

        method?.Invoke();
    }
}
