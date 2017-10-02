using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public int EnemyID = 0;

	private int EnemyHP;

	// Use this for initialization
	void Start () {
		if (EnemyID == 0) {
			EnemyHP = 3;
		}
		if (EnemyID == 1) {
			EnemyHP = 5;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (EnemyID == 0) {
			this.transform.Translate (-0.05f, 0f, 0f);
		}
		if (EnemyID == 1) {
			this.transform.Translate (-0.1f, 0f, 0f);
		}

		if (EnemyHP <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Wall") || other.gameObject.CompareTag ("Enemy")) {
			this.transform.Rotate (new Vector3 (0f, 0f, +180f));

		}
	}

	IEnumerator OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Bullet")) {
			yield return new WaitForSeconds (0.01f);
			EnemyHP--;
		}
	}


}
