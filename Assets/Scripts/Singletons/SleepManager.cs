using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class SleepManager : Singleton<SleepManager> {
    [SerializeField]
    private int maxSleep = 100;

    [SerializeField]
    private int minSleep;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SleepChanged;

    public int MinSleep => minSleep;
    public int MaxSleep => maxSleep;

    public decimal Sleep { get; private set; }

    private void OnEnable() {
        SleepChanged ??= new UnityEvent<MetricChangedArgs>();
        Sleep = maxSleep;
    }

    public void ReduceSanity(decimal amount) {
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

    public void IncreaseSanity(decimal amount) {
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

    private void OnSleepChanged(decimal oldSleep, decimal newSleep) {
        if (oldSleep != newSleep) {
            SleepChanged?.Invoke(new MetricChangedArgs(oldSleep, newSleep));
        }
    }
}