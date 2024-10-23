using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MainMenu : MonoBehaviour {
    [SerializeField]
    private AudioClip musicSane;

    [SerializeField]
    private AudioClip musicAnxious;

    [SerializeField]
    private AudioClip musicInsane;

    [SerializeField]
    [Tooltip("The change that a glitch occurs roughly every second. More than 1 glitch cannot occur at once.")]
    [Range(0.01f, 1)]
    private float glitchChance = 0.1f;

    [SerializeField]
    [Tooltip("The minimum duration of a glitch in seconds.")]
    [Range(0.1f, 25)]
    private float glitchMinDuration = 0.5f;

    [SerializeField]
    [Tooltip("The max duration of a glitch in seconds.")]
    [Range(0.1f, 25)]
    private float glitchMaxDuration = 5f;

    [SerializeField]
    [Tooltip("The chance that the insane music plays instead of the anxious music.")]
    [Range(0.01f, 1)]
    private float glitchInsaneMusicChance = 0.3f;

    [SerializeField]
    [Tooltip("FOR DEBUGGING ONLY - DO NOT EDIT.")]
    private float glitchAmount;

    [SerializeField]
    [Tooltip("FOR DEBUGGING ONLY - DO NOT EDIT.")]
    private bool glitchRunning;

    private void Start() {
        // For some reason, the BGM manager needs to be created separately before we can play any bgm.
        _ = BackgroundMusicManager.Instance;
        BackgroundMusicManager.Instance.ChangeBgmImmediate(musicSane);

        glitchAmount = GetRandomGlitchAmount();
    }

    private void Update() {
        CheckGlitchChance();
    }

    private void CheckGlitchChance() {
        glitchAmount = Mathf.Max(glitchAmount - Time.deltaTime, 0);

        if (glitchAmount == 0) {
            glitchAmount = GetRandomGlitchAmount();

            var bgm = Random.value > glitchInsaneMusicChance
                ? musicAnxious
                : musicInsane;

            if (!glitchRunning) {
                glitchRunning = true;
                StartCoroutine(RunGlitch(musicSane, bgm));
            }
        }
    }

    private float GetRandomGlitchAmount() {
        return Random.value / glitchChance * 2;
    }

    private IEnumerator RunGlitch(AudioClip oldBgm, AudioClip newBgm) {
        // TODO: Add visual changes to go along with music changes

        BackgroundMusicManager.Instance.ReplaceBgmImmediate(newBgm);

        var glitchLength = Random.Range(glitchMinDuration, glitchMaxDuration);
        yield return new WaitForSeconds(glitchLength);

        BackgroundMusicManager.Instance.ReplaceBgmImmediate(oldBgm);

        glitchRunning = false;
    }

    public void StartGame(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName);
    }
}