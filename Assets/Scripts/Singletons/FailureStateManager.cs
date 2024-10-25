using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class FailureStateManager : Singleton<FailureStateManager> {
    [SerializeField]
    [Tooltip("How many days of low sanity means failure. 0 = At the start of the next day.")]
    [Min(0)]
    private int lowSanityTooManyDaysFailure = 3;

    [SerializeField]
    [Tooltip("How many days of low sleep means failure. 0 = At the start of the next day.")]
    [Min(0)]
    private int lowSleepTooManyDaysFailure = 2;

    private int _ranOutOfSanityDay = -1;

    private int _ranOutOfSleepDay = -1;

    [NonSerialized]
    public UnityEvent<string> GameOver;

    private void OnEnable() {
        GameOver ??= new UnityEvent<string>();
        DateManager.Instance.DayChanged.AddListener(OnDateChanged);
    }

    private void OnDisable() {
        DateManager.Instance.DayChanged.RemoveListener(OnDateChanged);
    }

    private void OnDateChanged(DateChangedArgs e) {
        UpdateFailureTrackers();
        CheckFailure();
    }

    public void UpdateFailureTrackers() {
        var currentDay = DateManager.Instance.CurrentDate;

        var sanityManager = SanityManager.Instance;
        if (sanityManager.Sanity <= sanityManager.MinSanity) {
            if (_ranOutOfSanityDay == -1) {
                _ranOutOfSanityDay = currentDay;
            }
        }
        else {
            _ranOutOfSanityDay = -1;
        }

        var sleepManager = SleepManager.Instance;
        if (sleepManager.Sleep <= sleepManager.MinSleep) {
            if (_ranOutOfSleepDay == -1) {
                _ranOutOfSleepDay = currentDay;
            }
        }
        else {
            _ranOutOfSleepDay = -1;
        }
    }

    public void CheckFailure() {
        var healthManager = HealthManager.Instance;
        if (healthManager.Health <= healthManager.MinHealth) {
            OnGameOver("Ran out of health!");
            return;
        }

        var currentDay = DateManager.Instance.CurrentDate;
        if (currentDay - _ranOutOfSanityDay >= lowSanityTooManyDaysFailure) {
            OnGameOver("Ran out of sanity!");
            return;
        }

        if (currentDay - _ranOutOfSleepDay >= lowSleepTooManyDaysFailure) {
            OnGameOver("Ran out of sleep!");
        }
    }

    private void OnGameOver(string failureReason) {
        GameOver?.Invoke(failureReason);
    }
}