using UnityEngine;
using UnityEngine.SceneManagement;

public class WeekSummaryHandler : MonoBehaviour {
    private GameObject _uiContainer;

    private void OnEnable() {
        DateManager.Instance.DayChanged.AddListener(OnDayChanged);
    }

    private void OnDisable() {
        DateManager.Instance.DayChanged.RemoveListener(OnDayChanged);
    }

    private void Start() {
        ExpensesManager.Instance.AddExpense("Rent", 400);
        DontDestroySingleton.TryGetInstance("UIContainer", out _uiContainer);
    }

    private void OnDayChanged(DateChangedArgs e) {
        if (e.Date % 7 == 0) {
            LoadWeekSummaryScene();
        }
    }

    private void LoadWeekSummaryScene() {
        Time.timeScale = 0f; // pause the game
        SceneManager.LoadSceneAsync("WeekSummary");

        if (_uiContainer) {
            _uiContainer.SetActive(false);
        }
    }
}