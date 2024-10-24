using System;
using UnityEngine;

public abstract class LocationBase : MonoBehaviour {
    [SerializeField]
    protected InsaneMusicScriptableObject insaneMusicScriptableObject;

    [SerializeField]
    private AudioClip musicSane;

    [SerializeField]
    private AudioClip musicAnxious;

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
            MentalState.Sane => musicSane,
            MentalState.Anxious => musicAnxious,
            MentalState.Insane => insaneMusicScriptableObject.MusicInsane,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}