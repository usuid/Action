using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//public
	public float MaxHP = 100.0f;
	public float PlayerHP = 100.0f;
	public Slider PlayerHPBar;
	public Text PlayerHPtext;
	public float Speed = 1.0f;
	public bool CanMove = true;
	public float JumpSpeed = 10f;
	public GameObject Bullet;
	//private
	private bool canjump = true;
	private float xspeed;
	private bool isForwardRight;
	private bool isLadderStay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CanMove){
			xspeed = Input.GetAxis ("Horizontal") * Speed;
			this.transform.Translate (xspeed, 0f, 0f);
			if (Input.GetKey (KeyCode.RightArrow)) {
				isForwardRight = true;
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				isForwardRight = false;
			}

			if (!isLadderStay) {
				if (Input.GetKeyDown (KeyCode.UpArrow) && canjump) {
					this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * JumpSpeed);
					canjump = false;
				}
				if (Input.GetKeyUp (KeyCode.UpArrow) && this.GetComponent<Rigidbody2D> ().velocity.y >= 0f) {
					this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
				}
			} else {
				if (Input.GetKey (KeyCode.UpArrow)) {
					this.transform.Translate (new Vector3 (0f, 0.1f, 0f));
				}
				if (Input.GetKey (KeyCode.DownArrow)) {
					this.transform.Translate (new Vector3 (0f, -0.1f, 0f));
				}
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
			isLadderStay = false;
		}
		if (Other.gameObject.CompareTag ("Enemy")) {
			PlayerHP -= 1f;
			PlayerHPtext.text = PlayerHP.ToString() + "/"+ MaxHP.ToString();
			PlayerHPBar.value = PlayerHP / MaxHP;
		}
	}

	void OnTriggerStay2D (Collider2D coll){
		if (coll.gameObject.CompareTag ("Ladder")) {
			isLadderStay = true;
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
