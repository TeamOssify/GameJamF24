using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class MultiArtLocation : LocationBase {
    [SerializeField]
    private Sprite morningSaneSprite, morningAnxiousSprite, morningInsaneSprite;

    [SerializeField]
    private Sprite noonSaneSprite, noonAnxiousSprite, noonInsaneSprite;

    [SerializeField]
    private Sprite eveningSaneSprite, eveningAnxiousSprite, eveningInsaneSprite;

    [SerializeField]
    private Sprite nightSaneSprite, nightAnxiousSprite, nightInsaneSprite;

    [SerializeField]
    private GameObject locationArt;

    private Image _locationBackgroundImage;

    protected new virtual void Awake() {
        base.Awake();

        TimeManager.Instance.TimeOfDayChanged.AddListener(OnTimeOfDayChanged);
        _locationBackgroundImage = locationArt.GetComponent<Image>();
    }

    protected new virtual void Start() {
        base.Start();

        UpdateBackground();
    }

    protected new virtual void OnDestroy() {
        base.OnDestroy();

        TimeManager.Instance.TimeOfDayChanged.RemoveListener(OnTimeOfDayChanged);
    }

    protected new virtual void OnMentalStateChanged(MentalStateChangedArgs e) {
        base.OnMentalStateChanged(e);

        UpdateBackground();
    }

    protected virtual void OnTimeOfDayChanged(TimeOfDay e) {
        UpdateBackground();
    }

    private void UpdateBackground() {
        var background = GetBackground();
        _locationBackgroundImage.sprite = background;
    }

    private Sprite GetBackground() {
        var mentalState = MentalStateManager.Instance.CurrentMentalState;
        switch (TimeManager.Instance.CurrentTimeOfDay) {
            case TimeOfDay.Morning: {
                var sprite = mentalState switch {
                    MentalState.Sane => morningSaneSprite,
                    MentalState.Anxious => morningAnxiousSprite,
                    MentalState.Insane => morningInsaneSprite,
                    _ => null,
                };

                return sprite ? sprite : morningSaneSprite;
            }
            case TimeOfDay.Noon: {
                var sprite = mentalState switch {
                    MentalState.Sane => noonSaneSprite,
                    MentalState.Anxious => noonAnxiousSprite,
                    MentalState.Insane => noonInsaneSprite,
                    _ => null,
                };

                return sprite
                    ? sprite
                    : noonSaneSprite
                        ? noonSaneSprite
                        : morningSaneSprite;
            }
            case TimeOfDay.Evening: {
                var sprite = mentalState switch {
                    MentalState.Sane => eveningSaneSprite,
                    MentalState.Anxious => eveningAnxiousSprite,
                    MentalState.Insane => eveningInsaneSprite,
                    _ => null,
                };

                return sprite
                    ? sprite
                    : eveningSaneSprite
                        ? eveningSaneSprite
                        : morningSaneSprite;
            }
            case TimeOfDay.Night: {
                var sprite = mentalState switch {
                    MentalState.Sane => nightSaneSprite,
                    MentalState.Anxious => nightAnxiousSprite,
                    MentalState.Insane => nightInsaneSprite,
                    _ => null,
                };

                return sprite
                    ? sprite
                    : nightSaneSprite
                        ? nightSaneSprite
                        : morningSaneSprite;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}