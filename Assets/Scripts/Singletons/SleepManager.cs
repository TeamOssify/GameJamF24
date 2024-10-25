using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class SleepManager : Singleton<SleepManager> {
    [SerializeField]
    private float maxSleep = 100;

    [SerializeField]
    private float minSleep;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SleepChanged;

    //Sleep drain per frame
    public float sleepDrain = 2f;

    public float MinSleep => minSleep;
    public float MaxSleep => maxSleep;

    public float Sleep { get; private set; }

    private void OnEnable() {
        SleepChanged ??= new UnityEvent<MetricChangedArgs>();
        Sleep = maxSleep;
    }

    public void ReduceSleep(float amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to reduce sleep by a negative amount! ({0})", amount);
            return;
        }

        var oldSleep = Sleep;

        Sleep -= amount;
        if (Sleep < minSleep) {
            Sleep = minSleep;
        }

        OnSleepChanged(oldSleep, Sleep);
    }

    public void IncreaseSleep(float amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to increase sleep by a negative amount! ({0})", amount);
            return;
        }

        var oldSleep = Sleep;

        Sleep += amount;
        if (Sleep > maxSleep) {
            Sleep = maxSleep;
        }

        OnSleepChanged(oldSleep, Sleep);
    }

    private void OnSleepChanged(float oldSleep, float newSleep) {
        if (!Mathf.Approximately(oldSleep, newSleep)) {
            SleepChanged?.Invoke(new MetricChangedArgs(oldSleep, newSleep));
        }
    }
}