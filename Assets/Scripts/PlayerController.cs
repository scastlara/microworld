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
	public KeyCode left;
	public KeyCode right;
	public KeyCode jump;
	public KeyCode shoot;
	public Transform groundCheckPoint;
	public LayerMask whatIsGround;
	private Rigidbody2D theRB;


	void Start () {
		theRB = GetComponent<Rigidbody2D> ();
		jumpsDone = 0;
	}
	
	void Update () {

		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, roundCheckRadius);
		Debug.Log (groundCheckPoint.position);

		Debug.DrawLine (groundCheckPoint.position, theRB.transform.position, Color.blue, 100, false);
		if (!isGrounded) {
			Debug.Log (isGrounded);
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
}
