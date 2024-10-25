using System;
public sealed class HospitalLocation : MultiArtLocation { 
    public void RecieveTreatment() {
        if (BankAccountManager.Instance.Balance > 15 && SleepManager.Instance.Sleep > 15) {
            SleepManager.Instance.ReduceSleep(15);
            HealthManager.Instance.Heal(15);
            BankAccountManager.Instance.RemoveFunds(15);
        }
    }
}