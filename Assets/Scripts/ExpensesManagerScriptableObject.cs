using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ExpensesManagerScriptableObject : ScriptableObject {
    private List<Expense> _expenses;
    public IReadOnlyList<Expense> Expenses => _expenses;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> TotalExpensesChanged;

    private void OnEnable() {
        _expenses = new List<Expense>();
        TotalExpensesChanged ??= new UnityEvent<MetricChangedArgs>();
    }

    public void ClearExpenses() {
        var oldTotal = CalculateTotal();
        _expenses.Clear();
        var newTotal = CalculateTotal();

        OnTotalExpensesChanged(new MetricChangedArgs(oldTotal, newTotal));
    }

    public void AddExpense(string expenseName, decimal cost) {
        var oldTotal = CalculateTotal();
        _expenses.Add(new Expense(expenseName, cost));
        var newTotal = CalculateTotal();

        OnTotalExpensesChanged(new MetricChangedArgs(oldTotal, newTotal));
    }

    public decimal CalculateTotal() {
        return _expenses.Sum(x => x.Cost);
    }

    protected virtual void OnTotalExpensesChanged(MetricChangedArgs e) {
        if (e.OldValue != e.NewValue) {
            TotalExpensesChanged?.Invoke(e);
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