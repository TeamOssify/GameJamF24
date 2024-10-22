using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class DateManager : Singleton<DateManager> {
    [SerializeField]
    private DayOfWeek startingDay = DayOfWeek.Sunday;

    [NonSerialized]
    public UnityEvent<DateChangedArgs> DayChanged;

    public int CurrentDate { get; private set; }

    public DayOfWeek CurrentDayOfWeek => (DayOfWeek)(CurrentDate % 7);

    private void OnEnable() {
        DayChanged ??= new UnityEvent<DateChangedArgs>();
        CurrentDate = (int)startingDay;
    }

    public void AdvanceDate() {
        CurrentDate++;

        OnDateChanged(CurrentDate, CurrentDayOfWeek);
    }

    public void SetDate(int date) {
        CurrentDate = date;

        OnDateChanged(CurrentDate, CurrentDayOfWeek);
    }

    private void OnDateChanged(int date, DayOfWeek dayOfWeek) {
        DayChanged?.Invoke(new DateChangedArgs(date, dayOfWeek));
    }
}