using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class MentalStateManager : Singleton<MentalStateManager> {
    [SerializeField]
    [Min(0)]
    private int anxiousThreshold = 33;

    [SerializeField]
    [Min(0)]
    private int insaneThreshold = 15;

    [NonSerialized]
    public UnityEvent<MentalStateChangedArgs> MentalStateChanged;

    public MentalState CurrentMentalState { get; private set; }

    private void OnEnable() {
        MentalStateChanged ??= new UnityEvent<MentalStateChangedArgs>();
        SanityManager.Instance.SanityChanged.AddListener(OnSanityChanged);
    }

    private void OnSanityChanged(MetricChangedArgs e) {
        ComputeMentalState(e.NewValue);
    }

    private void ComputeMentalState(float sanity) {
        var oldMentalState = CurrentMentalState;

        if (sanity < insaneThreshold) {
            CurrentMentalState = MentalState.Insane;
        }
        else if (sanity < anxiousThreshold) {
            CurrentMentalState = MentalState.Anxious;
        }
        else {
            CurrentMentalState = MentalState.Sane;
        }

        OnMentalStateChanged(oldMentalState, CurrentMentalState);
    }

    private void OnMentalStateChanged(MentalState oldMentalState, MentalState newMentalState) {
        if (oldMentalState != newMentalState) {
            MentalStateChanged?.Invoke(new MentalStateChangedArgs(oldMentalState, newMentalState));
        }
    }
}