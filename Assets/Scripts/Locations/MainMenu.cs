using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenu : MonoBehaviour {
    [SerializeField]
    private AudioClip musicSane, musicAnxious, musicInsane;

    [SerializeField]
    private Sprite saneSprite, anxiousSprite, insaneSprite;

    [SerializeField]
    private GameObject layoutArt;

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
    [Tooltip("The chance that an insane glitch happens instead of an anxious glitch.")]
    [Range(0.01f, 1)]
    private float glitchInsaneChance = 0.3f;

    [SerializeField]
    [Tooltip("FOR DEBUGGING ONLY - DO NOT EDIT.")]
    private float glitchAmount;

    [SerializeField]
    [Tooltip("FOR DEBUGGING ONLY - DO NOT EDIT.")]
    private bool glitchRunning;

    [SerializeField]
    private LocationManager locationManager;

    private Image _layoutArtImage;

    private GameObject _uiContainer;

    private void Awake() {
        InitManagers();
        _layoutArtImage = layoutArt.GetComponent<Image>();
    }

    private void Start() {
        BackgroundMusicManager.Instance.ChangeBgmImmediate(musicSane);

        glitchAmount = GetRandomGlitchAmount();
    }

    private void Update() {
        CheckGlitchChance();
    }

    private void InitManagers() {
        // This sucks, but it's what we're working with.
        _ = BackgroundMusicManager.Instance;
        _ = BankAccountManager.Instance;
        _ = DateManager.Instance;
        _ = ExpensesManager.Instance;
        _ = FailureStateManager.Instance;
        _ = HealthManager.Instance;
        _ = IncomeManager.Instance;
        _ = MentalStateManager.Instance;
        _ = SanityManager.Instance;
        _ = SoundEffectManager.Instance;
        _ = TimeManager.Instance;
    }

    private void CheckGlitchChance() {
        glitchAmount = Mathf.Max(glitchAmount - Time.deltaTime, 0);

        if (glitchAmount == 0) {
            glitchAmount = GetRandomGlitchAmount();

            var rng = Random.value;
            var bgm = rng > glitchInsaneChance
                ? musicAnxious
                : musicInsane;

            var background = rng > glitchInsaneChance
                ? anxiousSprite
                : insaneSprite;

            if (!glitchRunning) {
                glitchRunning = true;
                StartCoroutine(RunGlitch(musicSane, bgm, _layoutArtImage.sprite, background));
            }
        }
    }

    private float GetRandomGlitchAmount() {
        return Random.value / glitchChance * 2;
    }

    private IEnumerator RunGlitch(AudioClip oldBgm, AudioClip newBgm, Sprite oldSprite, Sprite newSprite) {
        BackgroundMusicManager.Instance.ReplaceBgmImmediate(newBgm);
        _layoutArtImage.sprite = newSprite;

        var glitchLength = Random.Range(glitchMinDuration, glitchMaxDuration);
        yield return new WaitForSeconds(glitchLength);

        BackgroundMusicManager.Instance.ReplaceBgmImmediate(oldBgm);
        _layoutArtImage.sprite = oldSprite;

        glitchRunning = false;
    }

    public void StartGame(string sceneName) {
        DontDestroySingleton.TryGetInstance("UIContainer", out _uiContainer);
        if (_uiContainer)
            _uiContainer.gameObject.SetActive(true);

        locationManager.ChangeScene(sceneName);
        Time.timeScale = 1f;
        
    }
}