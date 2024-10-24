using System.Collections.Generic;
using UnityEngine;

public sealed class DontDestroySingleton : MonoBehaviour {
    private static readonly Dictionary<string, GameObject> Instances = new();

    [SerializeField]
    [Tooltip("The unique key used to identify this singleton.")]
    private string instanceKey;

    private void Awake() {
        if (string.IsNullOrWhiteSpace(instanceKey)) {
            Debug.LogErrorFormat("Null or empty instance key for {0} in {1}!", gameObject.name, gameObject.scene.name);
            return;
        }

        if (!Instances.ContainsKey(instanceKey)) {
            Instances.Add(instanceKey, gameObject);
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public static bool TryGetInstance(string key, out GameObject instance) {
        return Instances.TryGetValue(key, out instance);
    }
}