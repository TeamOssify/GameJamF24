using System;
using UnityEngine;

public abstract class LocationBase : MonoBehaviour {
    [SerializeField]
    protected GeneralMusicScriptableObject generalMusic;

    [SerializeField]
    private AudioClip musicSaneOverride;

    [SerializeField]
    private AudioClip musicAnxiousOverride;


    protected virtual void Awake() {
        MentalStateManager.Instance.MentalStateChanged.AddListener(OnMentalStateChanged);
    }

    protected virtual void Start() {
        UpdateMusic();
    }

    protected virtual void OnDestroy() {
        MentalStateManager.Instance.MentalStateChanged.RemoveListener(OnMentalStateChanged);
    }

    protected virtual void OnMentalStateChanged(MentalStateChangedArgs e) {
        UpdateMusic();
    }

    protected void UpdateMusic() {
        var music = GetMusicForMentalState();
        StartCoroutine(BackgroundMusicManager.Instance.ChangeBgmFade(music));
    }

    protected AudioClip GetMusicForMentalState() {
        return MentalStateManager.Instance.CurrentMentalState switch {
            MentalState.Sane => musicSaneOverride ? musicSaneOverride : generalMusic.MusicSane,
            MentalState.Anxious => musicAnxiousOverride ? musicAnxiousOverride : generalMusic.MusicAnxious,
            MentalState.Insane => generalMusic.MusicInsane,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}