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
        ExpensesManager.Instance.AddExpense("Rent", 400);
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
        
        BankAccountManager.Instance.RemoveFunds(400);
        if (BankAccountManager.Instance.Balance < 0) {
            ExpensesManager.Instance.ClearExpenses();
            FailureStateManager.Instance.GameOver?.Invoke("You couldn't afford to pay rent... living on the street is tough"); 
            return;
        }
        ExpensesManager.Instance.ClearExpenses();
        Time.timeScale = 1f;
        locationManager.ChangeLocation(Location.Map);

        if (_uiContainer) {
            _uiContainer.SetActive(true);
        }
    }
}