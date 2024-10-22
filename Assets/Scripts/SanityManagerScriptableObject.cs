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
        var oldSanity = Sanity;

        Sanity -= amount;
        if (Sanity < _minSanity) {
            Sanity = _minSanity;
        }

        OnSanityChanged(new MetricChangedArgs(oldSanity, Sanity));
    }

    public void IncreaseSanity(decimal amount) {
        var oldSanity = Sanity;

        Sanity += amount;
        if (Sanity > _maxSanity) {
            Sanity = _maxSanity;
        }

        OnSanityChanged(new MetricChangedArgs(oldSanity, Sanity));
    }

    protected virtual void OnSanityChanged(MetricChangedArgs e) {
        if (e.OldValue != e.NewValue) {
            SanityChanged?.Invoke(e);
        }
    }
}