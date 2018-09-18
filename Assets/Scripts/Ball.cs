using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private Paddle paddle;
    [SerializeField] private float verticalPush;
    [SerializeField] private float horizontalPush;
    [SerializeField] private AudioClip[] audioClips;

    private Vector2 distanceToPaddle;
    private bool isLocked;

    // Cached references
    private AudioSource audioSource;
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
        // This fixes the weird error with collision
        rigidBody.simulated = false;
        distanceToPaddle = transform.position - paddle.transform.position;
        isLocked = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isLocked)
        {
            LockToPaddle();
            // Launch ball if we do Left Click on the mouse
            if (Input.GetMouseButtonDown(0))
            {
                rigidBody.simulated = true;
                Launch();
            }
        }
    }

    private void LockToPaddle()
    {
        // Debug.Log("Ball Y Position: " + transform.position.y); // TODO Jmartnz Fix the error with Y position decreasing
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + distanceToPaddle;
    }

    private void Launch()
    {
        rigidBody.velocity = new Vector2(horizontalPush, verticalPush);
        isLocked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play random collision sound
        AudioClip clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(clip);

        // If paddle is collided, reset vertical push to avoid slow ball
        if (collision.gameObject.GetComponent<Paddle>())
        {
            Vector2 currentVelocity = rigidBody.velocity;
            rigidBody.velocity = new Vector2(currentVelocity.x, verticalPush); 
        }
    }
}
