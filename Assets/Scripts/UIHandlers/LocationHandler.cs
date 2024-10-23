using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationHandler : MonoBehaviour {
    private string _currentScene;
    private string _mapScene = "MainGame";

    void Start() {
        _currentScene = SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string sceneName) {
        SceneManager.LoadSceneAsync( sceneName );
    }

    public void Back() {
        if (SceneManager.GetActiveScene().name != null) {

            SceneManager.LoadSceneAsync(_mapScene);

        }
    }
}
