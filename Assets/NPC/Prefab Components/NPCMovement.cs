using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using Interface;

public class NPCMovement : MonoBehaviour {

	private Rigidbody2D theRigidbody;
	public float speed;
	public bool isMoving;
	private float moveTime;
	private float moveCounter;
	private float waitTime;
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
                ChooseWaitTime();
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
    /// <summary>
    /// Chooses randomly to move NPC left or right
    /// </summary>
	public void ChooseDirection()
	{
		moveDirection = Random.Range(0, 2);
		isMoving = true;
        ChooseMoveTime();
	}
    /// <summary>
    /// Chooses randomly how long NPC stays still between movements.
    /// </summary>
    public void ChooseWaitTime()
    {
        waitTime = Random.Range(1, 4);
        waitCounter = waitTime;
    }
    /// <summary>
    /// Chooses randomly how long the NPCs next movement lasts.
    /// </summary>
    public void ChooseMoveTime()
    {
        moveTime = Random.Range(1, 4);
        moveCounter = moveTime;
    }
}


