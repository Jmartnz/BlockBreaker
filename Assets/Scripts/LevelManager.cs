using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField] private Scene scene;

    public void StartGame()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
