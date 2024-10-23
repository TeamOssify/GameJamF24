using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    private void Start() {
        var Bank = BankAccountManager.Instance;
        moneyText.text = $"$: {Bank.Balance}";

        Bank.BalanceChanged.AddListener(OnBalanceChanged);
    }

    private void OnDestroy() {
        BankAccountManager.Instance.BalanceChanged.RemoveListener(OnBalanceChanged);
    }

    private void OnBalanceChanged(MetricChangedArgs args) {
        moneyText.text = $"$: {(float)args.NewValue}";
    }
}
