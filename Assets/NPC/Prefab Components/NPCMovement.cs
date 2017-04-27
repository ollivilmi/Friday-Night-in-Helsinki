using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using Interface;

public class NPCMovement : MonoBehaviour {

	private Rigidbody2D theRigidbody;
	private GameObject character;
	public float speed;
	public bool isMoving;
	private float moveTime;
	private float moveCounter;
	private float waitTime;
	private float waitCounter;
	private int moveDirection;
    private InterfaceManager iManager;
	public bool moveLeft { get; set; }
	public bool moveRight { get; set; }
	private Animator animator;
	private List<RuntimeAnimatorController> animations;
	private System.Random random;

	private void Start () {
		random = new System.Random ();
        iManager = FindObjectOfType<InterfaceManager>();
		theRigidbody = GetComponent<Rigidbody2D>();
		character = this.gameObject;
		animator = character.GetComponent<Animator> ();
		animations = new List<RuntimeAnimatorController> () {
			Resources.Load ("NPC_boy_blue") as RuntimeAnimatorController,
			Resources.Load ("NPC_boy_green") as RuntimeAnimatorController,
			Resources.Load ("NPC_boy_red") as RuntimeAnimatorController,
			Resources.Load ("NPC_girl_white") as RuntimeAnimatorController,
			Resources.Load ("NPC_girl_green") as RuntimeAnimatorController,
		};
		animator.runtimeAnimatorController = animations [random.Next (0, 5)];
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
				theRigidbody.velocity = new Vector2 (speed, 0);
				character.transform.eulerAngles = new Vector2 (0, 0);
				moveRight = true;
				//animator.SetBool ("moving", true);
				break;
			case 1:
				theRigidbody.velocity = new Vector2(-speed, 0);
				character.transform.eulerAngles = new Vector2 (0, 180);
				moveLeft = true;
				//animator.SetBool ("moving", true);
				break;
			}
			if(moveCounter < 0)
			{
				isMoving = false;
				animator.SetBool ("moving", false);
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
		animator.SetBool ("moving", true);
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


