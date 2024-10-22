using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityUI : MonoBehaviour {
    public Slider sanitySlider;


    void Start() {
        var sanityManager = SanityManager.Instance;
        sanitySlider.maxValue = (float)sanityManager.MaxSanity;
        sanitySlider.value = (float)sanityManager.Sanity;

        sanityManager.SanityChanged.AddListener(OnSanityChanged);
    }

    void OnDestroy() {
        SanityManager.Instance.SanityChanged.RemoveListener(OnSanityChanged);
    }

    private void OnSanityChanged(MetricChangedArgs args) {
        sanitySlider.value = (float)args.NewValue;
    }
}