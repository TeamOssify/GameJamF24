using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = nameof(LocationManager), menuName = "Scriptable Objects/Location Manager")]
public class LocationManager : ScriptableObject {
    [NonSerialized]
    public readonly UnityEvent<Location> LocationChanged = new();

    private static string GetSceneName(Location location) {
        return location switch {
            Location.Map => "LocationMap",
            Location.House => "HouseLocation",
            Location.Work => "WorkLocation",
            Location.Club => "ClubLocation",
            Location.Hospital => "HospitalLocation",
            _ => throw new ArgumentOutOfRangeException(nameof(location), location, null),
        };
    }

    public void ChangeScene(string sceneName) {
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (button != null) {
            var audioSource = button.GetComponent<AudioSource>();
            if (audioSource != null) {
                SoundEffectManager.Instance.PlaySoundEffect(audioSource.clip);
            }
        }
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ChangeScene(Location scene) {
        var sceneName = GetSceneName(scene);
        ChangeScene(sceneName);
    }

    private void OnLocationChanged(Location location) {
        LocationChanged?.Invoke(location);
    }
}
