using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private List<Block> blocks = new List<Block>();
    private int numberOfBlocks;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Breakable"))
        {
            blocks.Add(go.GetComponent<Block>());
        }
        numberOfBlocks = blocks.Count;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1-1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DecreaseNumberOfBlocks()
    {
        numberOfBlocks--;
        if (numberOfBlocks <= 0) { LoadNextLevel(); }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
