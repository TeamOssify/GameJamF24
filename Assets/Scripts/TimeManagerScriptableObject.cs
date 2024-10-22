using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManagerScriptableObject : ScriptableObject {
    [SerializeField]
    private readonly TimeSpan _startingTime = new(6, 0, 0);

    public TimeSpan CurrentTimeOfDay { get; private set; }

    [NonSerialized]
    public UnityEvent<TimeSpan> TimeChanged;

    private void OnEnable() {
        CurrentTimeOfDay = _startingTime;
        TimeChanged ??= new UnityEvent<TimeSpan>();
    }

    public void AdvanceTimeOfDay(TimeSpan deltaTime) {
        CurrentTimeOfDay += deltaTime;

        while (CurrentTimeOfDay.Days > 0) {
            CurrentTimeOfDay -= TimeSpan.FromDays(1);
        }

        OnTimeChanged(CurrentTimeOfDay);
    }

    protected virtual void OnTimeChanged(TimeSpan e) {
        TimeChanged?.Invoke(e);
    }
}