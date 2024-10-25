using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _failReasonText;

    void Start() {
        _failReasonText.text = GameOverHandler.FailureReason;

    }

    public void TryAgainButton() {
        SceneManager.LoadScene("Menu");
    }
}
