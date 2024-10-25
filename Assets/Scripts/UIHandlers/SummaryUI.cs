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
            summaryText.text = "Expenses:\n";
            foreach (var expense in ExpensesManager.Instance.Expenses) {
                summaryText.text += $"{expense.Name}: ${expense.Cost:N2}\n";
            }
        }
    }

    private void DisplayTitle() {
        title.text = $"Week {DateManager.Instance.CurrentDate / 7} Summary";
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        ExpensesManager.Instance.ClearExpenses();
        locationManager.ChangeLocation(Location.Map);

        if (_uiContainer) {
            _uiContainer.SetActive(true);
        }

        if (BankAccountManager.Instance.Balance < 0) {
            FailureStateManager.Instance.GameOver?.Invoke("You couldn't afford to pay rent... living on the street is tough");
        }
    }
}