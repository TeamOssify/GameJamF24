using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SummaryUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI SummaryText;
    [SerializeField] private TextMeshProUGUI Title;

    private GameObject UIContainer;

    void Start() {
        DontDestroySingleton.TryGetInstance("UIContainer", out UIContainer);
        BuildSummary();
        DisplayTitle();
    }

    void BuildSummary() {
        if (SummaryText) {
            var expensesManager = ExpensesManager.Instance;
            var expenses = expensesManager.Expenses;

            SummaryText.text = "Expenses:\n";
            foreach (var expense in expenses) {
                SummaryText.text += $"{expense.Name}: {expense.Cost:C}\n";
            }
        }
    }

    void DisplayTitle() {
        Title.text = $"Week " + DateManager.Instance.CurrentDate / 7 + " Summary";
    }


    public void ResumeGame() {
        Time.timeScale = 1f;
        if (UIContainer) {
            UIContainer.SetActive(true);
        }
        SceneManager.LoadSceneAsync("LocationMap");
    }
}
