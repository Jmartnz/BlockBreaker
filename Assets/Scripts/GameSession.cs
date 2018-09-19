﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [Range(0.1f, 2f)][SerializeField] private float gameSpeed = 1f;
    private int score = 0;

    // Cached references
    private Text scoreText;

    void Awake()
    {
        // Singleton
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

	// Use this for initialization
	void Start ()
    {
        scoreText = FindObjectOfType<Text>();
        scoreText.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Time.timeScale = gameSpeed;
    }

    public void SumPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
