﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//public
	public float MaxHP = 5.0f;
	public float PlayerHP = 5.0f;
	public Slider PlayerHPBar;
	public Text PlayerHPtext;
	public float Speed = 1.0f;
	public bool CanMove = true;
	public float JumpSpeed = 400f;
	public int BulletID;
	public GameObject[] Bullet;
	//private
	private bool canjump = true;
	private float moveSpeed;
	private bool isForwardRight;
	private bool isLadderStay;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		PlayerHPtext.text = PlayerHP.ToString() + "/"+ MaxHP.ToString();
		PlayerHPBar.value = PlayerHP / MaxHP;
		rb2d = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(CanMove){
			if (Input.GetKey (KeyCode.RightArrow)) {
				this.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
				if (rb2d.velocity.x <= 5f) {
					rb2d.AddForce (new Vector2 (30f, 0f));
				}
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.rotation = new Quaternion (0f, 180f, 0f, 0f);
				if (rb2d.velocity.x >= -5f) {
					rb2d.AddForce (new Vector2 (-30f, 0f));
				}
			}

			if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.LeftArrow)) {
				rb2d.velocity = new Vector2 (0f, rb2d.velocity.y);
			}

			if (Input.GetKeyDown (KeyCode.Z) && canjump) {
				rb2d.AddForce (Vector2.up * JumpSpeed);
				isLadderStay = false;
				rb2d.gravityScale = 1f;
				canjump = false;
			}
			if (Input.GetKeyUp (KeyCode.Z) && rb2d.velocity.y >= 0f) {
				rb2d.velocity = new Vector2 (0f, 0f);
			}

			if (isLadderStay) {
				if (Input.GetKey (KeyCode.UpArrow)) {
					if (rb2d.velocity.y <= 5f) {
						rb2d.AddForce (new Vector2 (0f, 30f));
					}
				}
				if (Input.GetKey (KeyCode.DownArrow)) {
					if (rb2d.velocity.y >= -5f) {
						rb2d.AddForce (new Vector2 (0f, -30f));
					}
				}
				if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.DownArrow)) {
					rb2d.velocity = new Vector2 (rb2d.velocity.x, 0f);
				}
			}

			if (Input.GetKeyDown (KeyCode.X)) {
				FireBullet ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D Other) {
		if (Other.gameObject.CompareTag ("Ground")) {
			isLadderStay = false;
			canjump = true;
			rb2d.gravityScale = 1f;
		}
		if (Other.gameObject.CompareTag ("Enemy")) {
			PlayerHP -= 1f;
			PlayerHPtext.text = PlayerHP.ToString() + "/"+ MaxHP.ToString();
			PlayerHPBar.value = PlayerHP / MaxHP;
		}
	}

	void OnTriggerStay2D (Collider2D coll){
		if (coll.gameObject.CompareTag ("Ladder")) {
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
				isLadderStay = true;
				canjump = true;
				rb2d.gravityScale = 0f;
			}
		}
	}

	void OnTriggerExit2D (Collider2D coll){
		if (coll.gameObject.CompareTag ("Ladder")) {
			isLadderStay = false;
			rb2d.gravityScale = 1f;
		}
	}

	void FireBullet () {
		Instantiate (Bullet [BulletID], transform.position, transform.rotation);
	}
}
