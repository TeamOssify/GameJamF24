public sealed class HospitalLocation : MultiArtLocation {
    public void ReceiveTreatment() {
        if (BankAccountManager.Instance.Balance > 15 && SleepManager.Instance.Sleep > 15) {
            SleepManager.Instance.ReduceSleep(15);
            HealthManager.Instance.Heal(20);
            BankAccountManager.Instance.RemoveFunds(200);
        }
    }
}