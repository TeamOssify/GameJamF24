using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component {
    private static T _instance;

    public static T Instance {
        get {
            if (!_instance) {
                var obj = new GameObject {
                    name = typeof(T).Name,
                    hideFlags = HideFlags.HideAndDontSave,
                };

                _instance = obj.AddComponent<T>();
            }

            return _instance;
        }
    }

    private void Awake() {
        if (Instance != this) {
            Destroy(gameObject);
        }
    }
}