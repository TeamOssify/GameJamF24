using UnityEngine;
using UnityEngine.UI;

public class SanityUI : MonoBehaviour {
    public Slider sanitySlider;

    private void Start() {
        var sanityManager = SanityManager.Instance;
        sanitySlider.maxValue = sanityManager.MaxSanity;
        sanitySlider.minValue = sanityManager.MinSanity;
        sanitySlider.value = (float)sanityManager.Sanity;

        sanityManager.SanityChanged.AddListener(OnSanityChanged);
    }

    private void OnDestroy() {
        SanityManager.Instance.SanityChanged.RemoveListener(OnSanityChanged);
    }

    private void OnSanityChanged(MetricChangedArgs args) {
        sanitySlider.value = (float)args.NewValue;
    }
}