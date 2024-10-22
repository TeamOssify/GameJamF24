using System;

public sealed record DateChangedArgs {
    public DateChangedArgs(int date, DayOfWeek dayOfWeek) {
        Date = date;
        DayOfWeek = dayOfWeek;
    }

    public int Date { get; }
    public DayOfWeek DayOfWeek { get; }
}