using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour {

    [SerializeField] private int maxHits = 1;
    [SerializeField] private int points = 10;
    [SerializeField] private bool breakable = true;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject sparklesVFX;

    private GameSession gameSession;
    private LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

    // TODO Consider making this list a SerializeField
    // Sprites to show block damage
    private List<Sprite> sprites;
    private int hits = 0;

    // Use this for initialization
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        levelManager = FindObjectOfType<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // TODO Find a better way for loading sprites
        sprites = Resources.LoadAll("Sprites/Block", typeof(Sprite)).Cast<Sprite>().ToList();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (breakable && collision.gameObject.GetComponent<Ball>())
        {
            TakeHit();
        }
    }

    private void TakeHit()
    {
        hits++;
        if (hits == maxHits)
        {
            DestroyBlock();
        }
        else if (hits >= maxHits / 2)
        {
            // Critical damage sprite
            spriteRenderer.sprite = sprites[1];
        }
        else
        {
            // Low damage sprite
            spriteRenderer.sprite = sprites[0];
        }
    }

    private void DestroyBlock()
    {
        if (gameSession == null) { gameSession = FindObjectOfType<GameSession>(); }
        AudioSource.PlayClipAtPoint(breakSound, transform.position);
        TriggerSparklesVFX();
        gameSession.SumPoints(points);
        levelManager.DecreaseNumberOfBlocks();
        Destroy(gameObject);
    }

    private void TriggerSparklesVFX()
    {
        Instantiate(sparklesVFX, transform.position, transform.rotation);
    }
}
