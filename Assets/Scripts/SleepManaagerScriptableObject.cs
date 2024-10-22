using System;
using UnityEngine;
using UnityEngine.Events;

public class SleepManaagerScriptableObject : ScriptableObject {
    [SerializeField]
    private readonly decimal _maxSleep = 100;

    [SerializeField]
    private readonly decimal _minSleep = 0;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SleepChanged;

    public decimal Sleep { get; private set; }

    private void OnEnable() {
        Sleep = _maxSleep;
        SleepChanged ??= new UnityEvent<MetricChangedArgs>();
    }

    public void ReduceSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to reduce sleep by a negative amount!");
            return;
        }

        var oldSleep = Sleep;

        Sleep -= amount;
        if (Sleep < _minSleep) {
            Sleep = _minSleep;
        }

        OnSleepChanged(new MetricChangedArgs(oldSleep, Sleep));
    }

    public void IncreaseSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to increase sleep by a negative amount!");
            return;
        }

        var oldSleep = Sleep;

        Sleep += amount;
        if (Sleep > _maxSleep) {
            Sleep = _maxSleep;
        }

        OnSleepChanged(new MetricChangedArgs(oldSleep, Sleep));
    }

    protected virtual void OnSleepChanged(MetricChangedArgs e) {
        if (e.OldValue != e.NewValue) {
            SleepChanged?.Invoke(e);
        }
    }
}