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