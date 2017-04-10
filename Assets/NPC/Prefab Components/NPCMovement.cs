using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class NPCMovement : MonoBehaviour {

	private Rigidbody2D theRigidbody;
	public float speed;
	public bool isMoving;
	public float moveTime;
	private float moveCounter;
	public float waitTime;
	private float waitCounter;
	private int moveDirection;
    private DialogueManager dManager;

	// Use this for initialization
	private void Start () {
        dManager = FindObjectOfType<DialogueManager>();
		theRigidbody = GetComponent<Rigidbody2D>();

		waitCounter = waitTime;
		moveCounter = moveTime;

		ChooseDirection();
	}

	// Update is called once per frame
	private void Update () {

		if (isMoving && !dManager.dialogueActive)
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


