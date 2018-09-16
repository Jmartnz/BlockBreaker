using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private Paddle paddle;
    [SerializeField] private float verticalPush;
    [SerializeField] private float horizontalPush;

    private Vector2 distanceToPaddle;
    private bool isLocked;

	// Use this for initialization
	void Start () {
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
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalPush, verticalPush);
        isLocked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Paddle>())
        {
            Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x, verticalPush); 
        }
    }
}
