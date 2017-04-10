using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementTest : MonoBehaviour {

	private Rigidbody2D theRigidbody;
	public float speed;
	public bool isMoving;
	public float moveTime;
	private float moveCounter;
	public float waitTime;
	private float waitCounter;
	private int moveDirection;

	// Use this for initialization
	void Start () {

		theRigidbody = GetComponent<Rigidbody2D>();

		waitCounter = waitTime;
		moveCounter = moveTime;

		ChooseDirection();
	}

	// Update is called once per frame
	void Update () {

		if (isMoving)
		{
			moveCounter -= Time.deltaTime;
			switch(moveDirection)
			{
			case 0:
				theRigidbody.velocity = new Vector2(speed,0);
				break;
			case 1:
				theRigidbody.velocity = new Vector2(-speed, 0);
				break;
			}
			if(moveCounter < 0)
			{
				isMoving = false;
				waitCounter = waitTime;
			}
		}
		else
		{
			waitCounter -= Time.deltaTime;
			theRigidbody.velocity = Vector2.zero;
			if (waitCounter < 0)
			{
				ChooseDirection();
			}
		}
	}

	public void ChooseDirection()
	{
		moveDirection = Random.Range(0, 2);
		isMoving = true;
		moveCounter = moveTime;
	}
}


