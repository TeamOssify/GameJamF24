using System;
using UnityEngine;

public sealed class ClubLocation : LocationBase {
    [SerializeField]
    private AudioClip clubMusicSane;

    [SerializeField]
    private AudioClip clubMusicAnxious;

    protected override AudioClip GetMusicForMentalState() {
        return MentalStateManager.Instance.CurrentMentalState switch {
            MentalState.Sane => clubMusicSane,
            MentalState.Anxious => clubMusicAnxious,
            MentalState.Insane => generalMusic.MusicInsane,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}