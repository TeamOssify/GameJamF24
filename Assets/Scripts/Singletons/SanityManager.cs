using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class SanityManager : Singleton<SanityManager> {
    [SerializeField]
    [Min(0)]
    private float maxSanity = 100;

    [SerializeField]
    [Min(0)]
    private float minSanity;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SanityChanged;

    //How much sanity drains every frame
    public float sanityDrain = 1f;

    public float MinSanity => minSanity;
    public float MaxSanity => maxSanity;

    public float Sanity { get; private set; }

    private void OnEnable() {
        SanityChanged ??= new UnityEvent<MetricChangedArgs>();
        Sanity = maxSanity;
    }

    public void ReduceSanity(float amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to reduce sanity by a negative amount! ({0})", amount);
            return;
        }

        float oldSanity = Sanity;

        Sanity -= amount;
        if (Sanity < minSanity) {
            Sanity = minSanity;
        }

        OnSanityChanged(oldSanity, Sanity);
    }

    public void IncreaseSanity(float amount) {
        if (amount < 0) {
            Debug.LogWarningFormat("Tried to increase sanity by a negative amount! ({0})", amount);
            return;
        }

        float oldSanity = Sanity;

        Sanity += amount;
        if (Sanity > maxSanity) {
            Sanity = maxSanity;
        }

        OnSanityChanged(oldSanity, Sanity);
    }

    private void OnSanityChanged(float oldSanity, float newSanity) {
        if (!Mathf.Approximately(oldSanity, newSanity)) {
            SanityChanged?.Invoke(new MetricChangedArgs(oldSanity, newSanity));
        }
    }
}