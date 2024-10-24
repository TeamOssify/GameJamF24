using System.Collections;
using UnityEngine;

public sealed class BackgroundMusicManager : Singleton<BackgroundMusicManager> {
    [SerializeField]
    [Tooltip("The time in seconds it takes for the BGM to fade in or out when switching tracks.")]
    [Min(0.01f)]
    private float bgmFadeTime = 0.75f;

    [SerializeField]
    [Range(0, 1)]
    private float bgmVolume = 1f;

    private AudioSource _audioSource;

    private void OnEnable() {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = bgmVolume;
        _audioSource.loop = true;
    }

    public IEnumerator ChangeBgmFade(AudioClip clip) {
        if (_audioSource.isPlaying) {
            yield return _audioSource.FadeOutToStop(bgmFadeTime / 2);
        }

        _audioSource.clip = clip;

        yield return _audioSource.FadeIn(bgmVolume, bgmFadeTime / 2);
    }

    public IEnumerator FadeBgmIn() {
        yield return _audioSource.FadeIn(bgmVolume, bgmFadeTime);
    }

    public IEnumerator FadeBgmOutToStop() {
        yield return _audioSource.FadeOutToStop(bgmFadeTime);
    }

    /// <summary>
    /// Changes the BGM immediately and resets the playback position
    /// </summary>
    /// <remarks>
    /// Do not use when the BGM may be fading between tracks.
    /// </remarks>
    public void ChangeBgmImmediate(AudioClip clip) {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.volume = bgmVolume;
        _audioSource.Play();
    }

    /// <summary>
    /// Changes the BGM immediately, but keeps playback position
    /// </summary>
    /// <remarks>
    /// Do not use when the BGM may be fading between tracks.
    /// </remarks>
    public void ReplaceBgmImmediate(AudioClip clip) {
        var offset = _audioSource.time;
        _audioSource.clip = clip;
        _audioSource.time = offset;
        _audioSource.volume = bgmVolume;

        if (!_audioSource.isPlaying) {
            _audioSource.Play();
        }
    }

    public void PauseBgm() {
        _audioSource.Pause();
    }

    public void UnpauseBgm() {
        _audioSource.volume = bgmVolume;
        _audioSource.UnPause();
    }
}