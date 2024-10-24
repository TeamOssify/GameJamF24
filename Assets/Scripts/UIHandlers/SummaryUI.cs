using TMPro;
using UnityEngine;

public class SummaryUI : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI summaryText;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private LocationManager locationManager;

    private GameObject _uiContainer;

    private void Start() {
        DontDestroySingleton.TryGetInstance("UIContainer", out _uiContainer);
        BuildSummary();
        DisplayTitle();
    }

    private void BuildSummary() {
        if (summaryText) {
            var expensesManager = ExpensesManager.Instance;
            var expenses = expensesManager.Expenses;

            summaryText.text = "Expenses:\n";
            foreach (var expense in expenses) {
                summaryText.text += $"{expense.Name}: {expense.Cost:C}\n";
            }
        }
    }

    private void DisplayTitle() {
        title.text = $"Week {DateManager.Instance.CurrentDate / 7} Summary";
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        if (_uiContainer) {
            _uiContainer.SetActive(true);
        }

        locationManager.ChangeLocation(Location.Map);
    }
}