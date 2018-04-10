using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlH : MonoBehaviour {
	public int HDirection=0; //0正面 1右邊 2後面 3左邊
	public float smooth = 5.0f; 
	public float tiltAngle = 90.0f;
	void Start(){
		
	}
	// Update is called once per frame
	void Update () {
		float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;

		Quaternion target = Quaternion.Euler(0, tiltAroundY, 0);
		this.transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
	}
}
