using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class HealthManager : Singleton<HealthManager> {
    [SerializeField]
    private float maxHealth = 100;

    [SerializeField]
    private float minHealth;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> HealthChanged;

    //Health damage every frame
    public float healthDrain = 0.002f;

    public float MinHealth => minHealth;
    public float MaxHealth => maxHealth;

    public float Health { get; private set; }

    private void OnEnable() {
        HealthChanged ??= new UnityEvent<MetricChangedArgs>();
        Health = maxHealth;
    }

    public void Damage(float amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to damage by a negative amount! ({0})", amount);
            return;
        }

        var oldHealth = Health;

        Health -= amount;
        if (Health < minHealth) {
            Health = minHealth;
        }

        OnHealthChanged(oldHealth, Health);
    }

    public void Heal(float amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to heal by a negative amount! ({0})", amount);
            return;
        }

        var oldHealth = Health;

        Health += amount;
        if (Health > maxHealth) {
            Health = maxHealth;
        }

        OnHealthChanged(oldHealth, Health);
    }

    private void OnHealthChanged(float oldHealth, float newHealth) {
        if (!Mathf.Approximately(oldHealth, newHealth)) {
            HealthChanged?.Invoke(new MetricChangedArgs(oldHealth, newHealth));
        }
    }
}