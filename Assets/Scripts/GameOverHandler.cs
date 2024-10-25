using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverHandler : MonoBehaviour {
    public static string FailureReason { get; private set; }
    private GameObject _uiContainer;

    private void OnEnable() {
        FailureStateManager.Instance.GameOver.AddListener(HandleGameOver);
    }

    private void OnDisable() {
        FailureStateManager.Instance.GameOver.RemoveListener(HandleGameOver);
    }

    private void HandleGameOver(string failureReason) {
        FailureReason = failureReason;
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver");
        DontDestroySingleton.TryGetInstance("UIContainer", out _uiContainer);
        if (_uiContainer)
            _uiContainer.SetActive(false);
        SanityManager.Instance.IncreaseSanity(SanityManager.Instance.MaxSanity);
        SleepManager.Instance.IncreaseSleep(SleepManager.Instance.MaxSleep);
        HealthManager.Instance.Heal(HealthManager.Instance.MaxHealth);
        if (BankAccountManager.Instance.Balance < 0)
            BankAccountManager.Instance.RemoveFunds(BankAccountManager.Instance.Balance);
        if (BankAccountManager.Instance.Balance > 0)
            BankAccountManager.Instance.AddFunds(BankAccountManager.Instance.Balance);
    }
}