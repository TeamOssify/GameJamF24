using System.Collections;
using UnityEngine;

public static class AudioSourceExtensions {
    public static IEnumerator FadeOutToStop(this AudioSource source, float duration) {
        if (duration <= 0) {
            source.Stop();
            yield break;
        }

        var startVolume = source.volume;

        while (source.volume > 0) {
            var delta = startVolume * Time.deltaTime / duration;
            source.volume -= Mathf.Clamp(delta, 0, startVolume);

            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }

    public static IEnumerator FadeIn(this AudioSource source, float volume, float duration) {
        if (duration <= 0) {
            if (!source.isPlaying) source.Play();

            yield break;
        }

        var startVolume = source.volume;
        source.volume = 0;

        if (!source.isPlaying) source.Play();

        while (source.volume > volume) {
            var delta = startVolume * Time.deltaTime / duration;
            source.volume += Mathf.Clamp(delta, 0, volume);

            yield return null;
        }

        source.volume = volume;
    }
}