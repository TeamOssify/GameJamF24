using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class BankAccountManager : Singleton<BankAccountManager> {
    [SerializeField]
    [Min(0)]
    private float initialBalance;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> BalanceChanged;

    public decimal Balance { get; private set; }

    private void OnEnable() {
        BalanceChanged ??= new UnityEvent<MetricChangedArgs>();
        Balance = (decimal)initialBalance;
    }

    public void AddFunds(decimal amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to add negative funds! ({0})", amount);
            return;
        }

        var oldBalance = Balance;
        Balance += amount;

        OnBalanceChanged((float)oldBalance, (float)Balance);
    }

    public void RemoveFunds(decimal amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to remove negative funds! ({0})", amount);
            return;
        }

        var oldBalance = Balance;
        Balance -= amount;

        OnBalanceChanged((float)oldBalance, (float)Balance);
    }

    public void OnBalanceChanged(float oldBalance, float newBalance) {
        if (!Mathf.Approximately(oldBalance, newBalance)) {
            BalanceChanged?.Invoke(new MetricChangedArgs(oldBalance, newBalance));
        }
    }
}