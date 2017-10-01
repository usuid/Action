﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("DestroyBullet");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.Translate (0.3f, 0f, 0f);
	}

	IEnumerator DestroyBullet (){
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}

}