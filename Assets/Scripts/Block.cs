using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject sparklesVFX;
    [SerializeField] private int hits;
    [SerializeField] private int points;

    // Cached reference
    private LevelManager levelManager;
    private GameSession gameStatus;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        gameStatus = FindObjectOfType<GameSession>();
        points = hits * 10;
    }

    void OnBecameVisible()
    {
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>()) { TakeHit(); }
    }

    private void TakeHit()
    {
        hits--;
        if (hits <= 0) { DestroyBlock(); }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, transform.position);
        Destroy(gameObject);
        levelManager.DecreaseNumberOfBlocks();
        gameStatus.SumPoints(points);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(sparklesVFX);
    }
}
