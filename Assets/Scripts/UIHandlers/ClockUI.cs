using System;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour {
    [SerializeField]
    private Transform hourHand, minuteHand;

    [SerializeField]
    private GameObject clock;

    [SerializeField]
    private Sprite clockSpriteNumbers, clockSpriteNoNumbers;

    private Image _clockImage;

    private void Awake() {
        _clockImage = clock.GetComponent<Image>();
    }

    private void Start() {
        InvokeRepeating(nameof(IncrementPassiveTime), 0f, 1f); //increment every 1 second
    }

    private void OnEnable() {
        MentalStateManager.Instance.MentalStateChanged.AddListener(OnMentalStateChanged);
        UpdateClockNumbers(MentalStateManager.Instance.CurrentMentalState);
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
                _clockImage.sprite = clockSpriteNumbers;
                break;
            case MentalState.Insane:
                _clockImage.sprite = clockSpriteNoNumbers;
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