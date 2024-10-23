using UnityEngine;

public class DontDestroyTag : MonoBehaviour {
    private static DontDestroyTag instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
