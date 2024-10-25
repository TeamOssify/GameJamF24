using System;
public sealed class WorkLocation : MultiArtLocation { 
    public void Work() {
        if (SanityManager.Instance.Sanity > 10 && SleepManager.Instance.Sleep > 15) {
            SanityManager.Instance.ReduceSanity(10);
            SleepManager.Instance.ReduceSleep(15);
            BankAccountManager.Instance.AddFunds(50);
            TimeManager.Instance.AdvanceTimeOfDay(TimeSpan.FromMinutes(180));
        }
    }
}