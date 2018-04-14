using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPaper : MonoBehaviour {
	private float timer =0;
	private bool clicked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit MouseHit;
		Ray MouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast (MouseRay, out MouseHit, 200.0f)) {
				if (MouseHit.transform != null && !clicked) {
					if (MouseHit.transform.gameObject.name == "ToiletPaper") {
						gameObject.GetComponent<AudioSource> ().Play ();
						clicked = true;
					}
				}
			}
		}
		if (clicked) {
			timer += Time.deltaTime;
			if (timer >= 1.0f) {
				Destroy (gameObject);
			}
		}
	}
}
