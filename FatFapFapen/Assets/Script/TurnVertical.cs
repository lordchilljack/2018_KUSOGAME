using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnVertical : MonoBehaviour {
	public float rotationRange = 100;
	public float rotationSpeed = 2.5f;
	public float dampingTime = 0.2f;

	private Vector3 targetAngles;
	private Vector3 followAngles;
	private Vector3 followVelocity;
	private Quaternion originalRotation;

	void Start()
	{
		originalRotation = transform.localRotation;
	}

	void Update()
	{
		transform.localRotation = originalRotation;

		float inputVertical = Input.GetAxis("Vertical");

		targetAngles.x += inputVertical * rotationSpeed;
		targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange * 0.5f, rotationRange * 0.5f);

		followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);
		transform.localRotation = originalRotation * Quaternion.Euler(-followAngles.x, followAngles.y, 0);
	}
}
