using System.Collections;
using UnityEngine;

public sealed class BackgroundMusicManager : Singleton<BackgroundMusicManager> {
    [SerializeField]
    [Tooltip("The time in seconds it takes for the BGM to fade in or out when switching tracks.")]
    [Min(0)]
    private float bgmFadeTime = 2f;

    private AudioSource _audioSource;

    private void OnEnable() {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
    }

    public IEnumerator ChangeBgmFade(AudioClip clip) {
        if (_audioSource.isPlaying) {
            yield return _audioSource.FadeOutToStop(bgmFadeTime);
        }

        _audioSource.clip = clip;
        yield return _audioSource.FadeIn(bgmFadeTime);
    }

    /// <summary>
    /// Changes the BGM immediately and resets the playback position
    /// </summary>
    public void ChangeBgmImmediate(AudioClip clip) {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    /// <summary>
    /// Changes the BGM immediately, but keeps playback position
    /// </summary>
    public void ReplaceBgmImmediate(AudioClip clip) {
        // TODO: Does this reset AudioSource.time to 0?
        _audioSource.clip = clip;
    }

    public void PauseBgm() {
        _audioSource.Pause();
    }

    public void UnpauseBgm() {
        _audioSource.UnPause();
    }
}