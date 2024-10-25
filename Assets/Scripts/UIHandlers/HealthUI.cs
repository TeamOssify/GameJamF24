using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    [SerializeField]
    private Slider healthSlider;

    private void Start() {
        var healthManager = HealthManager.Instance;
        healthSlider.maxValue = healthManager.MaxHealth;
        healthSlider.minValue = healthManager.MinHealth;
        healthSlider.value = healthManager.Health;

        healthManager.HealthChanged.AddListener(OnHealthChanged);
    }

    private void Update() {
        var healthManager = HealthManager.Instance;
        healthManager.Damage(healthManager.healthDrain * Time.deltaTime);
    }

    private void OnDestroy() {
        HealthManager.Instance.HealthChanged.RemoveListener(OnHealthChanged);
    }

    private void OnHealthChanged(MetricChangedArgs args) {
        healthSlider.value = args.NewValue;
    }
}