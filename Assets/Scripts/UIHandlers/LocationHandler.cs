using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationHandler : MonoBehaviour {
    private string _currentScene;

    void Start() {
        _currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }

    public void ChangeScene(string sceneName) {

        if (_currentScene != null) {
            SceneManager.UnloadSceneAsync(_currentScene);
            Debug.Log($"Unloading:" + _currentScene);
        }

        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _currentScene = sceneName;
    }
}