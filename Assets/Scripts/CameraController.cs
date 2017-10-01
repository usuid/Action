using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject Player;

	public bool AutoScroll = false;
	[SerializeField]
	private float scrollSpeed = 0.05f;

	[SerializeField]
	private float minX;
	[SerializeField]
	private float minY;
	[SerializeField]
	private float maxX = 100f;
	[SerializeField]
	private float maxY = 100f;

	private float positionX;
	private float positionY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//X座標の判断
		if (AutoScroll == false) {
			if (Player.transform.position.x <= minX) {
				positionX = minX;

			} else if (Player.transform.position.x >= maxX) {
				positionX = maxX;

			} else {
				positionX = Player.transform.position.x;
			}

			//Y座標の判断
			if (Player.transform.position.y <= minY) {
				positionY = minY;

			} else if (Player.transform.position.y >= maxY) {
				positionY = maxY;

			} else {
				positionY = Player.transform.position.y;

			}
			this.transform.position = new Vector3 (positionX, positionY, -10f);
		} else {
			positionX += scrollSpeed;
			if (this.transform.position.x >= maxX) {
				positionX = maxX;

			}

			this.transform.position = new Vector3 (positionX, this.transform.position.y, -10f);
		}
	}
}
