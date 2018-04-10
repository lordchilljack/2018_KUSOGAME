using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContralV : MonoBehaviour {
	public int VDirection=0; //0正面 1上面 2下面
	public float smooth = 5.0f; 
	public float tiltAngle = 90.0f;
	public float tiltAngleH = 180.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle*-1;
		float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngleH;
		Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, 0);
		this.transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
	}
}
