public sealed class HospitalLocation : MultiArtLocation {
    public void ReceiveTreatment() {
        if (BankAccountManager.Instance.Balance > 150 && SleepManager.Instance.Sleep > 15) {
            SleepManager.Instance.ReduceSleep(15);
            HealthManager.Instance.Heal(20);
            BankAccountManager.Instance.RemoveFunds(150);

            ExpensesManager.Instance.AddExpense("Visited the doctor", 150);
        }
    }
}