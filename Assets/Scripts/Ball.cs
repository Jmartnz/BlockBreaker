using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private float maxVelocity;
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
        // We want a straight up first shoot!
        rigidBody.velocity = new Vector2(0.0f, maxVelocity);
        isLocked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play random collision sound
        AudioClip clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(clip);

        // If ball is stuck on either X or Y axis, we apply some push to avoid endless bouncing
        float xPush = 0;
        float yPush = 0;
        Vector2 currentVelocity = rigidBody.velocity;

        // TODO Improve horizontal push to avoid near-zero velocity
        // Horizontal push
        if (currentVelocity.x == 0.0f)
        {
            do {
                xPush = UnityEngine.Random.Range(-2.0f, 2.0f);
            } while (xPush == 0.0f);
        }

        // Downward push
        if (currentVelocity.y == 0.0f || (currentVelocity.y > -1.0f && currentVelocity.y <= 0.0f))
        {
            yPush = UnityEngine.Random.Range(-3.0f, -2.0f);
        }
        // Upward push
        else if (currentVelocity.y > 0.0f && currentVelocity.y <= 1.0f)
        {
            yPush = UnityEngine.Random.Range(2.0f, 3.0f);
        }

        // We clamp the velocity of the ball in order to avoid absurd speeds and improve playability.
        rigidBody.velocity = new Vector2(
            Mathf.Clamp(currentVelocity.x + xPush, -maxVelocity, maxVelocity),
            Mathf.Clamp(currentVelocity.y + yPush, -maxVelocity, maxVelocity));
    }

}
