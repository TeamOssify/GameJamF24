using UnityEngine;
using UnityEngine.UI;

public class SleepUI : MonoBehaviour {
    [SerializeField]
    private Slider sleepSlider;

    private void Start() {
        var sleepManager = SleepManager.Instance;
        sleepSlider.maxValue = sleepManager.MaxSleep;
        sleepSlider.minValue = sleepManager.MinSleep;
        sleepSlider.value = sleepManager.Sleep;

        sleepManager.SleepChanged.AddListener(OnSleepChanged);
    }

    private void Update() {
        var sleepManager = SleepManager.Instance;
        sleepManager.ReduceSleep(sleepManager.sleepDrain * Time.deltaTime);
    }

    private void OnDestroy() {
        SleepManager.Instance.SleepChanged.RemoveListener(OnSleepChanged);
    }

    private void OnSleepChanged(MetricChangedArgs args) {
        sleepSlider.value = args.NewValue;
    }
}