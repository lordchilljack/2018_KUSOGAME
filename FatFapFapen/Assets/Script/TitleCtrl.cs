using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour {

	public GameObject EndlessB;
	public GameObject NightMB;
	public GameObject EasyB;
	public GameObject ShowB;

	public void LeaveGame(){
		Application.Quit ();
	}

	public void ChangeDisplay(){
		EndlessB.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		NightMB.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		EasyB.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
	}

    public void AddFalling(GameObject Item){
		if (Item.GetComponent<Rigidbody2D>() == null){
	        Rigidbody2D Rb;
	        BoxCollider2D bc;
	        Rb = Item.AddComponent<Rigidbody2D>();
	        bc = Item.AddComponent<BoxCollider2D>();
	        float Fx = Random.Range(-100.0f, +100.0f) * 300;
	        float Fy = Random.Range(-100.0f, +100.0f) * 300;
	        Rb.mass = 1;
	        Rb.AddForce(new Vector2(Fx, Fy));
        }
    }

	public void StartGame(){
		SceneManager.LoadScene(1);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
