using System;
using System.Collections.Generic;
using UnityEngine.Events;

public sealed class IncomeManager : Singleton<IncomeManager> {
    private List<IncomeSource> _incomeSources;

    [NonSerialized]
    public UnityEvent IncomeSourcesChanged;

    public IReadOnlyList<IncomeSource> IncomeSources => _incomeSources;

    private void OnEnable() {
        IncomeSourcesChanged ??= new UnityEvent();
        _incomeSources = new List<IncomeSource>();
        DateManager.Instance.DayChanged.AddListener(OnDateChanged);
    }

    private void OnDisable() {
        DateManager.Instance.DayChanged.RemoveListener(OnDateChanged);
    }

    private void OnDateChanged(DateChangedArgs e) {
        var income = CheckIncomeSources(e.Date);
        BankAccountManager.Instance.AddFunds((decimal)income);
    }

    private float CheckIncomeSources(int date) {
        float income = 0;
        var dirty = false;

        for (var i = 0; i < _incomeSources.Count; i++) {
            var incomeSource = _incomeSources[i];
            if (incomeSource.Date == date) {
                _incomeSources.RemoveAt(i);
                income += incomeSource.Amount;
                dirty = true;
            }
        }

        if (dirty) {
            OnIncomeSourcesChanged();
        }

        return income;
    }

    public void AddFutureIncomeSource(int date, float amount) {
        _incomeSources.Add(new IncomeSource(date, amount));
        OnIncomeSourcesChanged();
    }

    private void OnIncomeSourcesChanged() {
        IncomeSourcesChanged?.Invoke();
    }

    public sealed record IncomeSource {
        public IncomeSource(int date, float amount) {
            Date = date;
            Amount = amount;
        }

        public int Date { get; }
        public float Amount { get; }
    }
}