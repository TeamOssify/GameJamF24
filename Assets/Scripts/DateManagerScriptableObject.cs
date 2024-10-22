using System;
using UnityEngine;
using UnityEngine.Events;

public class DateManagerScriptableObject : ScriptableObject {
    [SerializeField]
    private DayOfWeek startingDay = DayOfWeek.Sunday;

    public int CurrentDate { get; private set; }

    [NonSerialized]
    public UnityEvent<DateChangedArgs> DayChanged;

    private void OnEnable() {
        CurrentDate = (int)startingDay;
        DayChanged ??= new UnityEvent<DateChangedArgs>();
    }

    public void AdvanceDate() {
        CurrentDate++;

        OnDateChanged(CurrentDate, (DayOfWeek)(CurrentDate % 7));
    }

    public void SetDate(int date) {
        CurrentDate = date;

        OnDateChanged(CurrentDate, (DayOfWeek)(CurrentDate % 7));
    }

    protected virtual void OnDateChanged(int date, DayOfWeek dayOfWeek) {
        DayChanged?.Invoke(new DateChangedArgs(date, dayOfWeek));
    }
}