using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _failReasonText;

    private void Start() {
        _failReasonText.text = GameOverHandler.FailureReason;
    }

    public void TryAgainButton() {
        SanityManager.Instance.IncreaseSanity(SanityManager.Instance.MaxSanity);
        SleepManager.Instance.IncreaseSleep(SleepManager.Instance.MaxSleep);
        HealthManager.Instance.Heal(HealthManager.Instance.MaxHealth);
        var currentBalance = BankAccountManager.Instance.Balance;
        if (currentBalance != 0) {
            if (currentBalance < 0)
                BankAccountManager.Instance.AddFunds(Math.Abs(currentBalance));
            else
                BankAccountManager.Instance.RemoveFunds(currentBalance);
        }
        ExpensesManager.Instance.ClearExpenses();
        
        TimeManager.Instance.SetTimeOfDay(TimeSpan.FromSeconds(0));
        DateManager.Instance.SetDate(0);
        SceneManager.LoadScene("Menu");
    }
}