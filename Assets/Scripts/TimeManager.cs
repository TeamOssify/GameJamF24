using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class TimeManager : Singleton<TimeManager> {
    [SerializeField]
    [Range(0, 23)]
    private int startingHour = 6;

    [SerializeField]
    [Range(0, 59)]
    private int startingMinute;

    [SerializeField]
    [Range(0, 59)]
    private int startingSecond;

    [NonSerialized]
    public UnityEvent<TimeSpan> TimeChanged;

    private TimeSpan StartingTime => new(startingHour, startingMinute, startingSecond);

    public TimeSpan CurrentTimeOfDay { get; private set; }

    private void OnEnable() {
        TimeChanged ??= new UnityEvent<TimeSpan>();
        CurrentTimeOfDay = StartingTime;
    }

    public void AdvanceTimeOfDay(TimeSpan deltaTime) {
        if (deltaTime.Ticks < 0) {
            Debug.LogWarning("Tried to advance the time of day by a negative time!");
            return;
        }

        if (deltaTime == TimeSpan.Zero) {
            return;
        }

        CurrentTimeOfDay += deltaTime;

        if (CurrentTimeOfDay.Days > 0) {
            CurrentTimeOfDay -= TimeSpan.FromDays(CurrentTimeOfDay.Days);
        }

        OnTimeChanged(CurrentTimeOfDay);
    }

    public void SetTimeOfDay(TimeSpan time) {
        if (time.Ticks < 0) {
            Debug.LogWarning("Tried to set the time of day to a negative time!");
            return;
        }

        CurrentTimeOfDay = time;

        if (CurrentTimeOfDay.Days > 0) {
            CurrentTimeOfDay -= TimeSpan.FromDays(CurrentTimeOfDay.Days);
        }

        OnTimeChanged(CurrentTimeOfDay);
    }

    private void OnTimeChanged(TimeSpan e) {
        TimeChanged?.Invoke(e);
    }
}