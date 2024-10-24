using UnityEngine;
using UnityEngine.UI;

public sealed class ClubLocation : LocationBase {
    [SerializeField]
    [Range(0.1f, 5f)]
    private float backgroundChangeSpeed = 1.5f;

    [SerializeField]
    private Sprite lightsSprite1, lightsSprite2, lightsSprite3;

    [SerializeField]
    private GameObject locationArt;

    private float _backgroundProgression;

    private int _currentBackground;

    private Image _locationBackgroundImage;

    protected override void Awake() {
        base.Awake();

        _locationBackgroundImage = locationArt.GetComponent<Image>();
    }

    protected override void Start() {
        base.Start();

        UpdateBackground();
    }

    private void Update() {
        UpdateBackground();
    }

    private void UpdateBackground() {
        var background = GetBackground();
        _locationBackgroundImage.sprite = background;
    }

    private Sprite GetBackground() {
        _backgroundProgression += Time.deltaTime;

        if (_backgroundProgression >= backgroundChangeSpeed) {
            _backgroundProgression -= backgroundChangeSpeed;

            _currentBackground++;
            if (_currentBackground > 2) {
                _currentBackground = 0;
            }
        }

        return _currentBackground switch {
            0 => lightsSprite1,
            1 => lightsSprite2,
            _ => lightsSprite3,
        };
    }
}