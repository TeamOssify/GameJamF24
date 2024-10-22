using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public sealed class ExpensesManager : Singleton<ExpensesManager> {
    private List<Expense> _expenses;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> TotalExpensesChanged;

    public IReadOnlyList<Expense> Expenses => _expenses;

    private void OnEnable() {
        TotalExpensesChanged ??= new UnityEvent<MetricChangedArgs>();
        _expenses = new List<Expense>();
    }

    public void ClearExpenses() {
        var oldTotal = CalculateTotal();
        _expenses.Clear();
        var newTotal = CalculateTotal();

        OnTotalExpensesChanged(oldTotal, newTotal);
    }

    public void AddExpense(string expenseName, decimal cost) {
        var oldTotal = CalculateTotal();
        _expenses.Add(new Expense(expenseName, cost));
        var newTotal = CalculateTotal();

        OnTotalExpensesChanged(oldTotal, newTotal);
    }

    public decimal CalculateTotal() {
        return _expenses.Sum(x => x.Cost);
    }

    private void OnTotalExpensesChanged(decimal oldTotal, decimal newTotal) {
        if (oldTotal != newTotal) {
            TotalExpensesChanged?.Invoke(new MetricChangedArgs(oldTotal, newTotal));
        }
    }

    public record Expense {
        public Expense(string name, decimal cost) {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public decimal Cost { get; }
    }
}