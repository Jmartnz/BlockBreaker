using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private float verticalPush;
    [SerializeField] private float horizontalPush;
    [SerializeField] private Paddle paddle;
    [SerializeField] private AudioClip[] audioClips;

    private AudioSource audioSource;
    private Rigidbody2D rigidBody;

    private Vector2 distanceToPaddle;
    private bool isLocked;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.simulated = false; // This fixes the weird error with lose collision at the start
        audioSource = GetComponent<AudioSource>();
        distanceToPaddle = transform.position - paddle.transform.position;
        isLocked = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isLocked)
        {
            LockToPaddle();
            // Launch ball if we do Left Click on the mouse
            if (Input.GetMouseButtonDown(0))
            {
                Launch();
            }
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + distanceToPaddle;
    }

    private void Launch()
    {
        rigidBody.simulated = true;
        rigidBody.velocity = new Vector2(horizontalPush, verticalPush);
        isLocked = false;
        // TODO Add some random horizontal push
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
