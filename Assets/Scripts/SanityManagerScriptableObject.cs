using System;
using UnityEngine;
using UnityEngine.Events;

public class SanityManagerScriptableObject : ScriptableObject {
    [SerializeField]
    private readonly decimal _maxSanity = 100;

    [SerializeField]
    private readonly decimal _minSanity = 0;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SanityChanged;

    public decimal Sanity { get; private set; }

    private void OnEnable() {
        Sanity = _maxSanity;
        SanityChanged ??= new UnityEvent<MetricChangedArgs>();
    }

    public void ReduceSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to reduce sanity by a negative amount!");
            return;
        }

        var oldSanity = Sanity;

        Sanity -= amount;
        if (Sanity < _minSanity) {
            Sanity = _minSanity;
        }

        OnSanityChanged(oldSanity, Sanity);
    }

    public void IncreaseSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to increase sanity by a negative amount!");
            return;
        }

        var oldSanity = Sanity;

        Sanity += amount;
        if (Sanity > _maxSanity) {
            Sanity = _maxSanity;
        }

        OnSanityChanged(oldSanity, Sanity);
    }

    protected virtual void OnSanityChanged(decimal oldSanity, decimal newSanity) {
        if (oldSanity != newSanity) {
            SanityChanged?.Invoke(new MetricChangedArgs(oldSanity, newSanity));
        }
    }
}