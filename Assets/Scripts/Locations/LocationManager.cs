using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = nameof(LocationManager), menuName = "Scriptable Objects/Location Manager")]
public class LocationManager : ScriptableObject {
    [NonSerialized]
    public readonly UnityEvent<Location?> LocationChanged = new();

    public Location CurrentLocation { get; private set; }

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

    private static Location? GetLocationName(string sceneName) {
        return sceneName switch {
            "LocationMap" => Location.Map,
            "HouseLocation" => Location.House,
            "WorkLocation" => Location.Work,
            "ClubLocation" => Location.Club,
            "HospitalLocation" => Location.Hospital,
            _ => null,
        };
    }

    public void ChangeScene(string sceneName) {
        if (GetLocationName(sceneName) is { } location) {
            CurrentLocation = location;
            OnLocationChanged(location);
        }
        else {
            OnLocationChanged(null);
        }

        ChangeSceneInternal(sceneName);
    }

    public void ChangeLocation(Location location) {
        CurrentLocation = location;
        OnLocationChanged(location);

        var sceneName = GetSceneName(location);
        ChangeSceneInternal(sceneName);
    }

    private static void ChangeSceneInternal(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void OnLocationChanged(Location? location) {
        LocationChanged?.Invoke(location);
    }
}