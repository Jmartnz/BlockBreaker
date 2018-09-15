using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private Paddle paddle;
    private Vector2 distance;

	// Use this for initialization
	void Start () {
        distance = transform.position - paddle.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        LockToPaddle();
    }

    private void LockToPaddle()
    {
        Debug.Log("Ball Y Position: " + transform.position.y);
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = (paddlePosition + distance);
    }
}
