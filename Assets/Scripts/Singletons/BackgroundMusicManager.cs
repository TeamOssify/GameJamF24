using System.Collections;
using UnityEngine;

public sealed class BackgroundMusicManager : Singleton<BackgroundMusicManager> {
    [SerializeField]
    [Tooltip("The time in seconds it takes for the BGM to fade in or out when switching tracks.")]
    [Min(0.01f)]
    private float bgmFadeTime = 1f;

    private AudioSource _audioSource;

    // This isn't thread-safe, but it's close enough
    private bool _isFading;

    private void OnEnable() {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
    }

    private IEnumerator CheckNotFading() {
        while (_isFading) {
            yield return null;
        }
    }

    public IEnumerator ChangeBgmFade(AudioClip clip) {
        yield return CheckNotFading();

        if (_audioSource.isPlaying) {
            _isFading = true;
            yield return _audioSource.FadeOutToStop(bgmFadeTime / 2);

            _isFading = false;
        }

        yield return CheckNotFading();

        _audioSource.clip = clip;
        _isFading = true;
        yield return _audioSource.FadeIn(bgmFadeTime / 2);

        _isFading = false;
    }

    public IEnumerator FadeBgmIn() {
        yield return CheckNotFading();

        _isFading = true;
        yield return _audioSource.FadeIn(bgmFadeTime);

        _isFading = false;
    }

    public IEnumerator FadeBgmOutToStop() {
        yield return CheckNotFading();

        _isFading = true;
        yield return _audioSource.FadeOutToStop(bgmFadeTime);

        _isFading = false;
    }

    /// <summary>
    /// Changes the BGM immediately and resets the playback position
    /// </summary>
    /// ///
    /// <remarks>
    /// Do not use when the BGM may be fading between tracks.
    /// </remarks>
    public void ChangeBgmImmediate(AudioClip clip) {
        _audioSource.Stop();
        _audioSource.clip = clip;
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

        if (!_audioSource.isPlaying) {
            _audioSource.Play();
        }
    }

    public void PauseBgm() {
        _audioSource.Pause();
    }

    public void UnpauseBgm() {
        _audioSource.UnPause();
    }
}