using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = nameof(SleepManagerScriptableObject), menuName = "ScriptableObjects/Sleep Manager")]
public sealed class SleepManagerScriptableObject : ScriptableObject {
    [SerializeField]
    private int maxSleep = 100;

    [SerializeField]
    private int minSleep = 0;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SleepChanged;

    public decimal Sleep { get; private set; }

    private void OnEnable() {
        Sleep = maxSleep;
        SleepChanged ??= new UnityEvent<MetricChangedArgs>();
    }

    public void ReduceSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to reduce sleep by a negative amount!");
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
            Debug.LogWarning("Tried to increase sleep by a negative amount!");
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