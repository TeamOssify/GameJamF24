using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = nameof(TimeManagerScriptableObject), menuName = "ScriptableObjects/Time Manager")]
public sealed class TimeManagerScriptableObject : ScriptableObject {
    [SerializeField]
    [Range(0, 23)]
    private int startingHour = 6;

    [SerializeField]
    [Range(0, 59)]
    private int startingMinute = 0;

    [SerializeField]
    [Range(0, 59)]
    private int startingSecond = 0;

    private TimeSpan StartingTime => new(startingHour, startingMinute, startingSecond);

    public TimeSpan CurrentTimeOfDay { get; private set; }

    [NonSerialized]
    public UnityEvent<TimeSpan> TimeChanged;

    private void OnEnable() {
        CurrentTimeOfDay = StartingTime;
        TimeChanged ??= new UnityEvent<TimeSpan>();
    }

    public void AdvanceTimeOfDay(TimeSpan deltaTime) {
        if (deltaTime.Ticks < 1) {
            return; // Zero or negative time was passed.
        }

        CurrentTimeOfDay += deltaTime;

        while (CurrentTimeOfDay.Days > 0) {
            CurrentTimeOfDay -= TimeSpan.FromDays(1);
        }

        OnTimeChanged(CurrentTimeOfDay);
    }

    public void SetTimeOfDay(TimeSpan time) {
        CurrentTimeOfDay = time;

        while (CurrentTimeOfDay.Days > 0) {
            CurrentTimeOfDay -= TimeSpan.FromDays(1);
        }

        OnTimeChanged(CurrentTimeOfDay);
    }

    private void OnTimeChanged(TimeSpan e) {
        TimeChanged?.Invoke(e);
    }
}