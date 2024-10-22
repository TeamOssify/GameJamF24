using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class HealthManager : Singleton<HealthManager> {
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int minHealth;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> HealthChanged;

    public int MinHealth => minHealth;

    public decimal Health { get; private set; }

    private void OnEnable() {
        HealthChanged ??= new UnityEvent<MetricChangedArgs>();
        Health = maxHealth;
    }

    public void Damage(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to damage by a negative amount!");
            return;
        }

        var oldHealth = Health;

        Health -= amount;
        if (Health < minHealth) {
            Health = minHealth;
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
        if (Health > maxHealth) {
            Health = maxHealth;
        }

        OnHealthChanged(oldHealth, Health);
    }

    private void OnHealthChanged(decimal oldHealth, decimal newHealth) {
        if (oldHealth != newHealth) {
            HealthChanged?.Invoke(new MetricChangedArgs(oldHealth, newHealth));
        }
    }
}