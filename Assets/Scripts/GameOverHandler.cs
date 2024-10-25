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
    }
}