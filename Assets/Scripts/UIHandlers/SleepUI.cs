using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepUI : MonoBehaviour {
    public Slider sleepSlider;


    void Start() {
        var sleepManager = SleepManager.Instance;
        sleepSlider.maxValue = (float)sleepManager.MaxSleep;
        sleepSlider.value = (float)sleepManager.Sleep;

        sleepManager.SleepChanged.AddListener(OnSleepChanged);
    }

    void OnDestroy() {
        SleepManager.Instance.SleepChanged.RemoveListener(OnSleepChanged);
    }

    private void OnSleepChanged(MetricChangedArgs args) {
        sleepSlider.value = (float)args.NewValue;
    }
}