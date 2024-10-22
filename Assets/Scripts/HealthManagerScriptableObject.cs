using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class HealthManagerScriptableObject : ScriptableObject {
    [SerializeField]
    private readonly decimal _maxHealth = 100;

    [SerializeField]
    private readonly decimal _minHealth = 0;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> HealthChanged;

    public decimal Health { get; private set; }

    private void OnEnable() {
        Health = _maxHealth;
        HealthChanged ??= new UnityEvent<MetricChangedArgs>();
    }

    public void Damage(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to damage by a negative amount!");
            return;
        }

        var oldHealth = Health;

        Health -= amount;
        if (Health < _minHealth) {
            Health = _minHealth;
        }

        OnHealthChanged(oldHealth, Health);
    }

    public void Heal(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to heal by a negative amount!");
            return;
        }

        var oldHealth = Health;

        Health += amount;
        if (Health > _maxHealth) {
            Health = _maxHealth;
        }

        OnHealthChanged(oldHealth, Health);
    }

    private void OnHealthChanged(decimal oldHealth, decimal newHealth) {
        if (oldHealth != newHealth) {
            HealthChanged?.Invoke(new MetricChangedArgs(oldHealth, newHealth));
        }
    }
}