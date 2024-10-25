using UnityEngine;
using UnityEngine.UI;

public class SanityUI : MonoBehaviour {
    [SerializeField]
    private Slider sanitySlider;

    private void Start() {
        var sanityManager = SanityManager.Instance;
        sanitySlider.maxValue = sanityManager.MaxSanity;
        sanitySlider.minValue = sanityManager.MinSanity;
        sanitySlider.value = sanityManager.Sanity;

        sanityManager.SanityChanged.AddListener(OnSanityChanged);
    }

    private void Update() {
        var sanityManager = SanityManager.Instance;
        sanityManager.ReduceSanity(sanityManager.sanityDrain * Time.deltaTime);
    }

    private void OnDestroy() {
        SanityManager.Instance.SanityChanged.RemoveListener(OnSanityChanged);
    }

    private void OnSanityChanged(MetricChangedArgs args) {
        sanitySlider.value = args.NewValue;
    }
}