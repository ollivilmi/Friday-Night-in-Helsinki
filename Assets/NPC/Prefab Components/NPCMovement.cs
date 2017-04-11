using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using Interface;

public class NPCMovement : MonoBehaviour {

	private Rigidbody2D theRigidbody;
	public float speed;
	public bool isMoving;
	public float moveTime;
	private float moveCounter;
	public float waitTime;
	private float waitCounter;
	private int moveDirection;
    private InterfaceManager iManager;

	private void Start () {
        iManager = FindObjectOfType<InterfaceManager>();
		theRigidbody = GetComponent<Rigidbody2D>();

		waitCounter = waitTime;
		moveCounter = moveTime;

		ChooseDirection();
	}

	private void Update () {

		if (isMoving && !iManager.dialogueActive)
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


