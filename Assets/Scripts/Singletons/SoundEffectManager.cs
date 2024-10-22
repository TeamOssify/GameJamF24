using UnityEngine;

public sealed class SoundEffectManager : Singleton<SoundEffectManager> {
    private AudioSource _audioSource;

    private void OnEnable() {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }

    public void StopAllSoundEffects() {
        _audioSource.Stop();
    }
}