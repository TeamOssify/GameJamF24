using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class SanityManager : Singleton<SanityManager> {
    [SerializeField]
    [Min(0)]
    private int maxSanity = 100000;

    [SerializeField]
    [Min(0)]
    private int minSanity;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SanityChanged;

    //How much sanity drains every frame
    public int sanityDrain = 2;

    public int MinSanity => minSanity;
    public int MaxSanity => maxSanity;

    public decimal Sanity { get; private set; }

    private void OnEnable() {
        SanityChanged ??= new UnityEvent<MetricChangedArgs>();
        Sanity = maxSanity;
    }

    public void ReduceSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to reduce sanity by a negative amount! ({0})", amount);
            return;
        }

        var oldSanity = Sanity;

        Sanity -= amount;
        if (Sanity < minSanity) {
            Sanity = minSanity;
        }

        OnSanityChanged(oldSanity, Sanity);
    }

    public void IncreaseSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to increase sanity by a negative amount! ({0})", amount);
            return;
        }

        var oldSanity = Sanity;

        Sanity += amount;
        if (Sanity > maxSanity) {
            Sanity = maxSanity;
        }

        OnSanityChanged(oldSanity, Sanity);
    }

    private void OnSanityChanged(decimal oldSanity, decimal newSanity) {
        if (oldSanity != newSanity) {
            SanityChanged?.Invoke(new MetricChangedArgs(oldSanity, newSanity));
        }
    }
}