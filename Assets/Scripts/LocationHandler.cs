using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationHandler : MonoBehaviour {
    void Start() {
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }

    public void Interact(int LocationIndex) {
        Debug.Log("your in scene: " + LocationIndex);
        SceneManager.LoadSceneAsync(LocationIndex);
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }
}