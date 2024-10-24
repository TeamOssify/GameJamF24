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

    [NonSerialized]
    public UnityEvent<TimeOfDay> TimeOfDayChanged;

    private TimeSpan StartingTime => new(startingHour, startingMinute, startingSecond);

    public TimeSpan CurrentTime { get; private set; }

    public TimeOfDay CurrentTimeOfDay { get; private set; }

    private void OnEnable() {
        TimeChanged ??= new UnityEvent<TimeSpan>();
        TimeOfDayChanged ??= new UnityEvent<TimeOfDay>();
        CurrentTime = StartingTime;
    }

    public void AdvanceTimeOfDay(TimeSpan deltaTime) {
        if (deltaTime.Ticks < 0) {
            Debug.LogWarningFormat("Tried to advance the time of day by a negative time! ({0})", deltaTime);
            return;
        }

        if (deltaTime == TimeSpan.Zero) {
            return;
        }

        CurrentTime += deltaTime;

        if (CurrentTime.Days > 0) {
            CurrentTime -= TimeSpan.FromDays(CurrentTime.Days);
            DateManager.Instance.AdvanceDate();
        }

        OnTimeChanged(CurrentTime);

        CheckTimeOfDay();
    }

    public void SetTimeOfDay(TimeSpan time) {
        if (time.Ticks < 0) {
            Debug.LogWarningFormat("Tried to set the time of day to a negative time! ({0})", time);
            return;
        }

        CurrentTime = time;

        if (CurrentTime.Days > 0) {
            CurrentTime -= TimeSpan.FromDays(CurrentTime.Days);
            DateManager.Instance.AdvanceDate();
        }

        OnTimeChanged(CurrentTime);

        CheckTimeOfDay();
    }

    private void CheckTimeOfDay() {
        var oldTimeOfDay = CurrentTimeOfDay;
        CurrentTimeOfDay = CurrentTime.Hours switch {
            >= 21 => TimeOfDay.Night,
            >= 17 => TimeOfDay.Evening,
            >= 10 => TimeOfDay.Noon,
            >= 5 => TimeOfDay.Morning,
            _ => TimeOfDay.Night,
        };

        OnTimeOfDayChanged(oldTimeOfDay, CurrentTimeOfDay);
    }

    private void OnTimeChanged(TimeSpan e) {
        TimeChanged?.Invoke(e);
    }

    private void OnTimeOfDayChanged(TimeOfDay oldTimeOfDay, TimeOfDay newTimeOfDay) {
        if (oldTimeOfDay != newTimeOfDay) {
            TimeOfDayChanged?.Invoke(newTimeOfDay);
        }
    }
}