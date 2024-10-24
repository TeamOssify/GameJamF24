using UnityEngine;
using System;

public class ClockUI : MonoBehaviour {
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;

    private void Start() {
        InvokeRepeating(nameof(IncrementPassiveTime), 0f, 1f); //increment every 1 second
    }

    private void IncrementPassiveTime() {
        TimeManager.Instance.AdvanceTimeOfDay(TimeSpan.FromSeconds(60));
    }

    private void Update() {
        TimeSpan currentTime = TimeManager.Instance.CurrentTimeOfDay;

        float hours = (float)currentTime.TotalHours % 12;
        float minutes = (float)currentTime.TotalMinutes % 60;

        float hourAngle = hours * 30f; // 360 degrees / 12 hours
        float minuteAngle = minutes * 6f; // 360 degrees / 60 minutes

        if (hourHand != null) {
            hourHand.rotation = Quaternion.Euler(0, 0, -hourAngle);
        }
        if (minuteHand != null) {
            minuteHand.rotation = Quaternion.Euler(0, 0, -minuteAngle);
        }
    }
}
