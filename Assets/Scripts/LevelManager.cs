using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private Block[] blocks;
    private int numberOfBlocks;

    // Use this for initialization
    void Start()
    {
        blocks = FindObjectsOfType<Block>();
        numberOfBlocks = blocks.Length;
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
