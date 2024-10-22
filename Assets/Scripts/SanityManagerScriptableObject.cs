using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = nameof(SanityManagerScriptableObject), menuName = "ScriptableObjects/Sanity Manager")]
public sealed class SanityManagerScriptableObject : ScriptableObject {
    [SerializeField]
    private int maxSanity = 100;

    [SerializeField]
    private int minSanity = 0;

    [NonSerialized]
    public UnityEvent<MetricChangedArgs> SanityChanged;

    public decimal Sanity { get; private set; }

    private void OnEnable() {
        Sanity = maxSanity;
        SanityChanged ??= new UnityEvent<MetricChangedArgs>();
    }

    public void ReduceSanity(decimal amount) {
        if (amount < 0) {
            Debug.LogWarning("Tried to reduce sanity by a negative amount!");
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
            Debug.LogWarning("Tried to increase sanity by a negative amount!");
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