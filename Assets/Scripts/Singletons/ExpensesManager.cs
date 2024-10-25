using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public sealed class ExpensesManager : Singleton<ExpensesManager> {
    private Dictionary<string, decimal> _expenses;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> TotalExpensesChanged;

    public IEnumerable<Expense> Expenses => _expenses.Select(x => new Expense(x.Key, (float)x.Value));

    private void OnEnable() {
        TotalExpensesChanged ??= new UnityEvent<MetricChangedArgs>();
        _expenses = new Dictionary<string, decimal>();
    }

    public void ClearExpenses() {
        var oldTotal = CalculateTotal();
        _expenses.Clear();
        var newTotal = CalculateTotal();

        OnTotalExpensesChanged(oldTotal, newTotal);
    }

    public void AddExpense(string expenseName, float cost) {
        var oldTotal = CalculateTotal();
        if (_expenses.TryGetValue(expenseName, out var value)) {
            _expenses[expenseName] = (decimal)cost + value;
        }
        else {
            _expenses[expenseName] = (decimal)cost;
        }

        var newTotal = CalculateTotal();

        OnTotalExpensesChanged(oldTotal, newTotal);
    }

    public float CalculateTotal() {
        return (float)_expenses.Sum(x => x.Value);
    }

    private void OnTotalExpensesChanged(float oldTotal, float newTotal) {
        if (!Mathf.Approximately(oldTotal, newTotal)) {
            TotalExpensesChanged?.Invoke(new MetricChangedArgs(oldTotal, newTotal));
        }
    }

    public record Expense {
        public Expense(string name, float cost) {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public float Cost { get; }
    }
}