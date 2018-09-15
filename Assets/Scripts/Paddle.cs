using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    private int screenWidthInUnits = 16;
    private float paddleSize;

	// Use this for initialization
	void Start () {
        paddleSize = GetComponent<PolygonCollider2D>().bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () {
        float mouseXPosition = (Input.mousePosition.x / Screen.width * screenWidthInUnits);
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(mouseXPosition, (paddleSize / 2), screenWidthInUnits - (paddleSize / 2));

        transform.position = paddlePosition;
	}
}
