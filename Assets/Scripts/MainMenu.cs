using System.Collections;
using UnityEngine;

public sealed class MainMenu : MonoBehaviour {
    [SerializeField]
    private AudioClip musicClean;

    [SerializeField]
    private AudioClip musicScuffed;

    [SerializeField]
    private AudioClip musicCrushed;

    [SerializeField]
    [Tooltip("The change that a glitch occurs roughly every second. More than 1 glitch cannot occur at once.")]
    [Range(0.01f, 1)]
    private float glitchChance = 0.1f;

    [SerializeField]
    [Tooltip("The minimum duration of a glitch in seconds.")]
    [Range(0.1f, 10)]
    private float glitchMinDuration = 0.2f;

    [SerializeField]
    [Tooltip("The max duration of a glitch in seconds.")]
    [Range(0.1f, 10)]
    private float glitchMaxDuration = 3f;

    [SerializeField]
    [Tooltip("The chance that the crushed music plays instead of the scuffed music.")]
    [Range(0.01f, 1)]
    private float glitchCrushedMusicChance = 0.3f;

    private float _glitchAmount;

    private bool _glitchRunning = false;

    private void Start() {
        BackgroundMusicManagerSingleton.Instance.ChangeBgmImmediate(musicClean);
    }

    private void Update() {
        CheckGlitchChance();
    }

    private void CheckGlitchChance() {
        _glitchAmount = Mathf.Max(_glitchAmount - Time.deltaTime, 0);

        if (_glitchAmount == 0) {
            _glitchAmount = (Random.value / glitchChance) * 2;

            var bgm = Random.value > glitchCrushedMusicChance
                ? musicScuffed
                : musicCrushed;

            if (!_glitchRunning) {
                _glitchRunning = true;
                StartCoroutine(RunGlitch(musicClean, bgm));
            }
        }
    }

    private IEnumerator RunGlitch(AudioClip oldBgm, AudioClip newBgm) {
        // TODO: Add visual changes to go along with music changes

        BackgroundMusicManagerSingleton.Instance.ChangeBgmImmediate(newBgm);

        var glitchLength = Random.Range(glitchMinDuration, glitchMaxDuration);
        yield return new WaitForSeconds(glitchLength);

        BackgroundMusicManagerSingleton.Instance.ChangeBgmImmediate(oldBgm);

        _glitchRunning = false;
    }
}