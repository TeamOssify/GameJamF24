using UnityEngine;
using System;

public class ClockUI : MonoBehaviour {
    [SerializeField]
    private Transform hourHand;

    [SerializeField]
    private Transform minuteHand;

    [SerializeField]
    private GameObject clockNumbers;

    private void Start() {
        InvokeRepeating(nameof(IncrementPassiveTime), 0f, 1f); //increment every 1 second
        UpdateClockNumbers(MentalStateManager.Instance.CurrentMentalState);
    }

    private void OnEnable() {
        MentalStateManager.Instance.MentalStateChanged.AddListener(OnMentalStateChanged);
    }

    private void OnDisable() {
        MentalStateManager.Instance.MentalStateChanged.RemoveListener(OnMentalStateChanged);
    }

    private void OnMentalStateChanged(MentalStateChangedArgs e) {
        UpdateClockNumbers(e.NewState);
    }

    private void UpdateClockNumbers(MentalState mentalState) {
        switch (mentalState) {
            case MentalState.Sane:
            case MentalState.Anxious:
                clockNumbers.SetActive(true);
                break;
            case MentalState.Insane:
                clockNumbers.SetActive(false);
                break;
        }
    }

    private void IncrementPassiveTime() {
        TimeManager.Instance.AdvanceTimeOfDay(TimeSpan.FromSeconds(60));
    }

    private void Update() {
        TimeSpan currentTime = TimeManager.Instance.CurrentTime;

        float hours = (float)currentTime.TotalHours % 12;
        float minutes = (float)currentTime.TotalMinutes % 60;

        float hourAngle = hours * 30f; // 360 degrees / 12 hours
        float minuteAngle = minutes * 6f; // 360 degrees / 60 minutes

        if (hourHand) {
            hourHand.rotation = Quaternion.Euler(0, 0, -hourAngle);
        }

        if (minuteHand) {
            minuteHand.rotation = Quaternion.Euler(0, 0, -minuteAngle);
        }
    }
}