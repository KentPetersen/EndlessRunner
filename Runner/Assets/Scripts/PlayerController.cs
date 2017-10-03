using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float speedMultiplier;
	private float MoveSpeedStore;

	public float speedIncreaseMilestone;
	private float speedIncreaseMilestoneStore;

	private float speedMilestoneCount;
	private float speedMilestoneCountStore;

	public float jumpForce;

	public float jumpTime;
	private float jumpTimeCounter;

	private bool stoppedJumping;
	private bool canDoubleJump;

	public bool isDoubleJumpEquipped;
	public bool isGravityInvertedEquipped;

	private Rigidbody2D myRigidbody;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	//private Collider2D myCollider;

	private Animator myAnimator;

	public GameManager theGameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound;


	//*****************************
	//gravity inverting variables
	public bool isGravityInverted;
	//*****************************

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> (); 

		//myCollider = GetComponent<Collider2D> ();

		myAnimator = GetComponent<Animator> ();

		jumpTimeCounter = jumpTime;

		speedMilestoneCount = speedIncreaseMilestone;

		MoveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;

		stoppedJumping = true;


	}
	
	// Update is called once per frame
	void Update () {

		//grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedIncreaseMilestone;

			moveSpeed = moveSpeed * speedMultiplier;
			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;


		}

		myRigidbody.velocity = new Vector2 (moveSpeed,myRigidbody.velocity.y);

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			if (grounded) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play ();
			}

			if (!grounded ) {
				if (canDoubleJump && isDoubleJumpEquipped) {
					//double jump code
					myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
					jumpTimeCounter = jumpTime;
					stoppedJumping = false;
					canDoubleJump = false;
					jumpSound.Play ();
				}
				if (canDoubleJump && isGravityInvertedEquipped){

					//**************************
					//gravity code instead of double jump
				 
					isGravityInverted = !isGravityInverted;

					myRigidbody.gravityScale *=-1; //reverse current gravity
					jumpForce *= -1;

					//invert player art
					if (isGravityInverted) {
						this.transform.eulerAngles = new Vector3 (180, 0, 0);
					} else if (!isGravityInverted) {
						this.transform.eulerAngles = new Vector3 (0, 0, 0);
					}
				
					//**************************
				}
			
			}
		}

		if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) && !stoppedJumping) 
		{
			if (jumpTimeCounter > 0) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}


				
		}

		if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) {
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
			canDoubleJump = true;

		}

		myAnimator.SetFloat ("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);

		//**************************
		//gravity flip
		if (Input.GetKeyDown (KeyCode.Z))
		{
			isGravityInverted = !isGravityInverted;

			myRigidbody.gravityScale *=-1; //reverse current gravity
			jumpForce *= -1;

			//invert player art
			if (isGravityInverted) {
				this.transform.eulerAngles = new Vector3 (180, 0, 0);
			} else if (!isGravityInverted) {
				this.transform.eulerAngles = new Vector3 (0, 0, 0);
			}
		}
		//***********************

	

	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "KillBox") {
			theGameManager.RestartGame ();
			moveSpeed = MoveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
			deathSound.Play ();
		}
	}

	public void EquipGravityInverter()
	{
		isGravityInvertedEquipped = true;
		isDoubleJumpEquipped = false;
	}

	public void EquipDoubleJump()
	{
		isDoubleJumpEquipped = true;
		isGravityInvertedEquipped = false;
	}
		
}
