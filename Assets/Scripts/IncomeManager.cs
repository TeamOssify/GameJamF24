using System;
using System.Collections.Generic;
using UnityEngine.Events;

public sealed class IncomeManager : Singleton<IncomeManager> {
    private List<IncomeSource> _incomeSources;

    [NonSerialized]
    public UnityEvent<decimal> Income;

    public IReadOnlyList<IncomeSource> IncomeSources => _incomeSources;

    private void OnEnable() {
        Income ??= new UnityEvent<decimal>();
        _incomeSources = new List<IncomeSource>();
        DateManager.Instance.DayChanged.AddListener(OnDateChanged);
    }

    private void OnDateChanged(DateChangedArgs e) {
        var income = CheckIncomeSources(e.Date);
        OnIncome(income);
    }

    public decimal CheckIncomeSources(int date) {
        decimal income = 0;

        for (var i = 0; i < _incomeSources.Count; i++) {
            var incomeSource = _incomeSources[i];
            if (incomeSource.Date == date) {
                _incomeSources.RemoveAt(i);
                income += incomeSource.Amount;
            }
        }

        return income;
    }

    public void AddIncomeSource(int date, decimal amount) {
        _incomeSources.Add(new IncomeSource(date, amount));
    }

    private void OnIncome(decimal amount) {
        if (amount != 0) {
            Income?.Invoke(amount);
        }
    }

    public sealed record IncomeSource {
        public IncomeSource(int date, decimal amount) {
            Date = date;
            Amount = amount;
        }

        public int Date { get; }
        public decimal Amount { get; }
    }
}