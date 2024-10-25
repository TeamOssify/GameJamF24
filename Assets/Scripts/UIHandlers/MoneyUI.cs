using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void Start() {
        var bank = BankAccountManager.Instance;
        moneyText.text = $"${bank.Balance:N2}";

        bank.BalanceChanged.AddListener(OnBalanceChanged);
    }

    private void OnDestroy() {
        BankAccountManager.Instance.BalanceChanged.RemoveListener(OnBalanceChanged);
    }

    private void OnBalanceChanged(MetricChangedArgs args) {
        moneyText.text = $"${args.NewValue}";
    }
}