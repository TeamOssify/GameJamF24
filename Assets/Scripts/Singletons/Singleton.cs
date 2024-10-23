using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component {
    private static T _instance;

    public static T Instance {
        get {
            if (!_instance) {
                var obj = new GameObject {
                    name = typeof(T).Name,
                    hideFlags = HideFlags.DontSave
                };

                _instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj); // DDOL instead of hide flags so it shows in the editor
            }

            return _instance;
        }
    }

    protected virtual void Awake() {
        if (_instance && _instance != this) {
            Destroy(gameObject);
        }
    }
}