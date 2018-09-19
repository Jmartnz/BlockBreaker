using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour {

    [SerializeField] private int hits = 1;
    [SerializeField] private int points = 10;
    [SerializeField] private bool breakable = true;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject sparklesVFX;

    private GameSession gameSession;
    private LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

    // Sprites to show block damage
    private List<Sprite> sprites;

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
        hits--;
        if (hits > 1)
        {
            // Low damage sprite
            spriteRenderer.sprite = sprites[0];
        }
        else if (hits == 1)
        {
            // Critical damage sprite
            spriteRenderer.sprite = sprites[1];
        }
        else if (hits <= 0)
        {
            DestroyBlock();
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
        GameObject sparkles = Instantiate(sparklesVFX, transform.position, transform.rotation);
    }
}
