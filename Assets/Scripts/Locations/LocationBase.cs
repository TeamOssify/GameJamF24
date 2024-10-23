using System;
using UnityEngine;

public abstract class LocationBase : MonoBehaviour {
    [SerializeField]
    protected GeneralMusicScriptableObject generalMusic;

    [SerializeField]
    private AudioClip musicSaneOverride;

    [SerializeField]
    private AudioClip musicAnxiousOverride;

    private void Awake() {
        MentalStateManager.Instance.MentalStateChanged.AddListener(OnMentalStateChanged);
    }

    private void Start() {
        UpdateMusic();
    }

    private void OnDestroy() {
        MentalStateManager.Instance.MentalStateChanged.RemoveListener(OnMentalStateChanged);
    }

    private void OnMentalStateChanged(MentalStateChangedArgs e) {
        UpdateMusic();
    }

    protected AudioClip GetMusicForMentalState() {
        return MentalStateManager.Instance.CurrentMentalState switch {
            MentalState.Sane => musicSaneOverride ?? generalMusic.MusicSane,
            MentalState.Anxious => musicAnxiousOverride ?? generalMusic.MusicAnxious,
            MentalState.Insane => generalMusic.MusicInsane,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    protected void UpdateMusic() {
        var music = GetMusicForMentalState();
        StartCoroutine(BackgroundMusicManager.Instance.ChangeBgmFade(music));
    }
}