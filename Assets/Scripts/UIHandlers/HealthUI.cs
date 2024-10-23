using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    public Slider healthSlider;

    private void Start() {
        var healthManager = HealthManager.Instance;
        healthSlider.maxValue = healthManager.MaxHealth;
        healthSlider.minValue = healthManager.MinHealth;
        healthSlider.value = (float)healthManager.Health;

        healthManager.HealthChanged.AddListener(OnHealthChanged);
    }

    private void OnDestroy() {
        HealthManager.Instance.HealthChanged.RemoveListener(OnHealthChanged);
    }

    private void OnHealthChanged(MetricChangedArgs args) {
        healthSlider.value = (float)args.NewValue;
    }
}