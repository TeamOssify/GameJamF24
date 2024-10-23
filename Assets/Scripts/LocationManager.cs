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
            Location.Map => "MainGame",
            Location.House => "HouseLocation",
            Location.Work => "WorkLocation",
            Location.Club => "ClubLocation",
            _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
        };
    }

    public void ChangeScene(string sceneName) {
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