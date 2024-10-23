using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class BankAccountManager : Singleton<BankAccountManager> {
    [SerializeField]
    [Min(0)]
    private double initialBalance;

    private decimal _balance;

    public decimal Balance => _balance;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> BalanceChanged;

    private void OnEnable() {
        BalanceChanged ??= new UnityEvent<MetricChangedArgs>();
        _balance = (decimal)initialBalance;
    }

    public void AddFunds(decimal amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to add negative funds! ({0})", amount);
            return;
        }

        var oldBalance = _balance;
        _balance += amount;

        OnBalanceChanged(oldBalance, _balance);
    }

    public void RemoveFunds(decimal amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to remove negative funds! ({0})", amount);
            return;
        }

        var oldBalance = _balance;
        _balance -= amount;

        OnBalanceChanged(oldBalance, _balance);
    }

    public void OnBalanceChanged(decimal oldBalance, decimal newBalance) {
        if (oldBalance != newBalance) {
            BalanceChanged?.Invoke(new MetricChangedArgs(oldBalance, newBalance));
        }
    }
}