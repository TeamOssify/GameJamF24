using System;
public sealed class HouseLocation : MultiArtLocation { 
    public void Sleep() {
        SleepManager.Instance.IncreaseSleep(50);
        TimeManager.Instance.AdvanceTimeOfDay(TimeSpan.FromMinutes(360));
    }
    public void WatchTv() {
        if (SleepManager.Instance.Sleep > 5) {
            SanityManager.Instance.IncreaseSanity(5);
            SleepManager.Instance.ReduceSleep(5);
            TimeManager.Instance.AdvanceTimeOfDay(TimeSpan.FromMinutes(60));
        }
    }
}