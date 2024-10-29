using UnityEngine;
using UnityEngine.SceneManagement;

public class WeekSummaryHandler : MonoBehaviour {
    [SerializeField]
    private AudioClip saneMusic, anxiousMusic;

    private GameObject _uiContainer;

    private void OnEnable() {
        DateManager.Instance.DayChanged.AddListener(OnDayChanged);
    }

    private void OnDisable() {
        DateManager.Instance.DayChanged.RemoveListener(OnDayChanged);
    }

    private void Start() {
        var currentMentalState = MentalStateManager.Instance.CurrentMentalState;
        if (currentMentalState == MentalState.Sane) {
            StartCoroutine(BackgroundMusicManager.Instance.ChangeBgmFade(saneMusic));
        }
        else if (currentMentalState == MentalState.Anxious) {
            StartCoroutine(BackgroundMusicManager.Instance.ChangeBgmFade(anxiousMusic));
        }

        DontDestroySingleton.TryGetInstance("UIContainer", out _uiContainer);
    }

    private void OnDayChanged(DateChangedArgs e) {
        if (e.Date % 7 == 0) {
            LoadWeekSummaryScene();
        }
    }

    private void LoadWeekSummaryScene() {
        Time.timeScale = 0f; // pause the game
        
        
        SceneManager.LoadSceneAsync("WeekSummary");
        
        if (_uiContainer) {
            _uiContainer.SetActive(false);
        }
    }
}