using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public float roundCheckRadius;
	public bool isGrounded;
	public int jumpsMax;
	private int jumpsDone;
	public int currGems = 0;
	public int health = 3;
	public KeyCode left;
	public KeyCode right;
	public KeyCode jump;
	public KeyCode shoot;
	public Transform groundCheckPoint;
	public LayerMask whatIsGround;
	private Rigidbody2D theRB;
	private bool invincible = false;
	private int invTime = 3;
	Animator anim;
	private float collTime = -3;

	void Start () {
		theRB = GetComponent<Rigidbody2D> ();
		if (gameObject.tag == "Player2") {
			jumpsDone = 0;
			moveSpeed = 5;
			jumpForce = 5;
			jumpsMax = 1;
			roundCheckRadius = 0.05f;
			left = KeyCode.LeftArrow;
			right = KeyCode.RightArrow;
			jump = KeyCode.Space;
			whatIsGround = LayerMask.NameToLayer("Ground");
			foreach (Transform child in transform)
			{
				groundCheckPoint = child;
			}
		}
	}

	void Update () {
		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, roundCheckRadius, whatIsGround);
		//Debug.Log (groundCheckPoint.position);
		isInvincible (invTime);

		//Debug.DrawLine (groundCheckPoint.position, theRB.transform.position, Color.blue, 100, false);
		if (!isGrounded) {
			//Debug.Log (isGrounded);
		}

		if (isGrounded && (jumpsDone == jumpsMax)) {
			jumpsDone = 0;
		}

		if (Input.GetKey (left)) {
			theRB.velocity = new Vector2 (-moveSpeed, theRB.velocity.y);
		} else if (Input.GetKey (right)) {
			theRB.velocity = new Vector2 (moveSpeed, theRB.velocity.y);
		} else {
			theRB.velocity = new Vector2 (0, theRB.velocity.y);
		}
		if (Input.GetKey (jump) && isGrounded && (jumpsDone <= jumpsMax)) {

			theRB.velocity = new Vector2 (theRB.velocity.x, jumpForce);
			jumpsDone++;
		}

	}

	public bool isInvincible(int time) {
		float currTime = Time.time;
		if (currTime - collTime < time) {
			invincible = true;
		} else {
			invincible = false;
		}
		//Debug.Log ("CURR_TIME: " + currTime);
		//Debug.Log ("COLL_TIME: " + collTime);
		//Debug.Log ("Invincibility is "+invincible);
		return invincible;
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.collider.tag == "Enemy") {
			if (gameObject.tag == "Player1") {



				// añadir atributos a player 2 en base a los guardados

				// TAKING DAMAGE WHEN TOUCHING ENEMIES
				if (health > 0) {
					if (!isInvincible (invTime)) {
						health--;
						collTime = Time.time;
						Debug.Log ("HERE WE ARE");
						Debug.Log ("SMASH " + health + " lifes");
					} else {
						//Debug.Log ("YOU ARE SAFE");
					}
				} else {
					Destroy (gameObject);
				}

			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
        Debug.Log(coll.collider.tag);
		if (coll.collider.tag == "redGem") {
            Debug.Log("HOLA");
			currGems += 1;
			Destroy (coll.gameObject);
			Debug.Log (coll.collider.tag);

		}
	}
}
