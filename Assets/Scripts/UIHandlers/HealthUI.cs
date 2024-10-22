using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    public Slider healthSlider;
   

    void Start() {
        var healthManager = HealthManager.Instance;
        healthSlider.maxValue = (float)healthManager.MaxHealth;
        healthSlider.value = (float)healthManager.Health;

        healthManager.HealthChanged.AddListener(OnHealthChanged);
    }

    void OnDestroy() {
        HealthManager.Instance.HealthChanged.RemoveListener(OnHealthChanged);
    }

    private void OnHealthChanged(MetricChangedArgs args) {
        healthSlider.value = (float)args.NewValue;
    }
}