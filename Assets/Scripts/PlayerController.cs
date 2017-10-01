using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//public
	public float PlayerHP = 100.0f;
	public float Speed = 1.0f;
	public bool CanMove = true;
	public float JumpSpeed = 10f;
	public GameObject Bullet;
	//private
	private bool canjump = true;
	private float xspeed;
	private bool isForwardRight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(CanMove){
			xspeed = Input.GetAxis ("Horizontal") * Speed;
			this.transform.Translate (xspeed, 0f, 0f);
			if (Input.GetKey (KeyCode.RightArrow)) {
				isForwardRight = true;
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				isForwardRight = false;
			}

			if (Input.GetKeyDown (KeyCode.UpArrow) && canjump) {
				this.GetComponent<Rigidbody2D>().AddForce (Vector2.up * JumpSpeed);
				canjump = false;
			}
			if (Input.GetKeyUp (KeyCode.UpArrow) && this.GetComponent<Rigidbody2D> ().velocity.y >= 0f) {
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				FireNormalBullet ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D Other) {
		if (Other.gameObject.CompareTag ("Ground")) {
			canjump = true; 
		}
	}
	void FireNormalBullet () {
		if (isForwardRight) {
			Instantiate (Bullet, transform.position, transform.rotation);
		} else {
			Instantiate (Bullet, transform.position, new Quaternion(0f,180f,0f,0f));
		}
	}
}
