using UnityEngine;

public abstract class LocationBase : MonoBehaviour {
    [SerializeField]
    protected GeneralMusicScriptableObject generalMusic;

    private void Awake() {
        MentalStateManager.Instance.MentalStateChanged.AddListener(OnMentalStateChanged);
    }

    private void Start() {
        UpdateMusic();
    }

    private void OnDestroy() {
        MentalStateManager.Instance.MentalStateChanged.RemoveListener(OnMentalStateChanged);
    }

    private void OnMentalStateChanged(MentalStateChangedArgs e) {
        UpdateMusic();
    }

    protected virtual AudioClip GetMusicForMentalState() {
        return generalMusic.GetMusicForMentalState();
    }

    protected void UpdateMusic() {
        var music = GetMusicForMentalState();
        StartCoroutine(BackgroundMusicManager.Instance.ChangeBgmFade(music));
    }
}