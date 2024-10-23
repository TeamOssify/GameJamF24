using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationHandler : MonoBehaviour {
    private string _currentScene;
    private string _mapScene = "MainGame";
   
    void Start() {
        _currentScene = _mapScene;
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }

    public void ChangeScene(string sceneName) {
        

        if (_currentScene != null) {
            SceneManager.UnloadSceneAsync(_currentScene);
            Debug.Log($"Unloading:" + _currentScene);
        }

        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadSceneAsync(sceneName);

        _currentScene = sceneName;
    }

    public void Back() {
        if (SceneManager.GetActiveScene().name != null) {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

            SceneManager.LoadSceneAsync(_mapScene);
            Debug.Log($"Returning to:" + _mapScene);
        }


    }
}