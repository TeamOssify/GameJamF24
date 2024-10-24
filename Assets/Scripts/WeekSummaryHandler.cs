using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class WeekSummaryHandler : MonoBehaviour {
    private GameObject UIContainer;
    private int daysPassed = 0;

    private void OnEnable() {
        DateManager.Instance.DayChanged.AddListener(OnDayChanged);
    }

    private void OnDisable() {
        DateManager.Instance.DayChanged.RemoveListener(OnDayChanged);
    }

    private void Start() {
        DontDestroySingleton.TryGetInstance("UIContainer", out UIContainer);
    }

    private void OnDayChanged(DateChangedArgs args) {
        daysPassed++;
        if (daysPassed % 7 == 0) {
            LoadWeekSummaryScene();
        }
    }

    private void LoadWeekSummaryScene() {
        Time.timeScale = 0f; // pause the game
        if (UIContainer) {
            UIContainer.SetActive(false);
        }
        SceneManager.LoadSceneAsync("WeekSummary");
    }
}
