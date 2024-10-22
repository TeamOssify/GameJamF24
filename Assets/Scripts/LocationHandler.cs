using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationHandler : MonoBehaviour
{
    public void Interact(int LocationIndex) {
        Debug.Log("your in scene: " + LocationIndex);
        SceneManager.LoadSceneAsync(LocationIndex);
    }

}
