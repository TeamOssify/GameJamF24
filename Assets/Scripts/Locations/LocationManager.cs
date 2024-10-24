using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
        var button = EventSystem.current.currentSelectedGameObject;
        if (button) {
            var audioSource = button.GetComponent<AudioSource>();
            if (audioSource) {
                SoundEffectManager.Instance.PlaySoundEffect(audioSource.clip);
            }
        }

        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ChangeLocation(Location location) {
        var sceneName = GetSceneName(location);
        ChangeScene(sceneName);
    }

    private void OnLocationChanged(Location location) {
        LocationChanged?.Invoke(location);
    }
}
