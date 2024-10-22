using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = nameof(HealthManagerScriptableObject), menuName = "ScriptableObjects/Health Manager")]
public sealed class HealthManagerScriptableObject : ScriptableObject {
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int minHealth = 0;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> HealthChanged;

    public decimal Health { get; private set; }

    private void OnEnable() {
        Health = maxHealth;
        HealthChanged ??= new UnityEvent<MetricChangedArgs>();
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