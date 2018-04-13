using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PCCtrl: MonoBehaviour {

	public GameObject DeskTop;
	public GameObject PornMainWindow;
	public GameObject PornPause;
	public GameObject MinBar;

	public string NowPointName = "none";
	public int PCState = 0;// 0 DeskTop 1 Playing 2 Pause
	public bool PornStatePause = true;

	// Use this for initialization
	void Start () {
		DeskTop.transform.localPosition= new Vector3(-176.0f,69.45f,520.0f);
		PornMainWindow.transform.localPosition = new Vector3 (-176.0f,67.099f,525.97f);
		MinBar.transform.localPosition = new Vector3 (-62.23f,-35.3f,-3.55f);
		PornPause.transform.localScale = new Vector3 (0.0f,0.0f,0.0f);
	}

	//
	//
	//
	public void PrintObjName(GameObject Go){
		print (Go.name);
		NowPointName = Go.name;
	}
	// Update is called once per frame
	void Update () {
		GameObject[] PPlayer = GameObject.FindGameObjectsWithTag ("PornPlayer");
		RaycastHit MouseHit;
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(-1)) { // 修正 UI 層的碰撞偵測。
			Ray MouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (MouseRay , out MouseHit, 200.0f)) {
				if (MouseHit.transform != null) {
					PrintObjName(MouseHit.transform.gameObject);
					NowPointName = MouseHit.transform.gameObject.name;
				}
			}
		}
		if (PCState == 0) {
			if (NowPointName == "PornMinBar") {
				DeskTop.transform.localPosition= new Vector3(-176.0f,69.45f,529.8f);
				MinBar.transform.localPosition = new Vector3 (-62.23f,-35.3f,2.69f);
				PornMainWindow.transform.localPosition = new Vector3 (0.0f,2.36f,0.441f);
				if (PornStatePause) {
					PornPause.transform.localScale = new Vector3 (-15.0f,1.0f,8.0f);
					PCState = 2;
				} else {
					PCState = 1;
				}
			}
		} else if (PCState == 1) {
			if (NowPointName == "PornMain") {
				PornPause.transform.localScale = new Vector3 (-15.0f, 1.0f, 8.0f);
				PPlayer [0].GetComponent<UnityEngine.Video.VideoPlayer> ().Pause ();
				PornStatePause = true;
				PCState = 2;
			} else if (NowPointName == "PronMin") {
				PornPause.transform.localScale = new Vector3 (0.0f,0.0f,0.0f);
				DeskTop.transform.localPosition= new Vector3(-176.0f,69.45f,520.0f);
				PornMainWindow.transform.localPosition = new Vector3 (-176.0f,67.099f,525.97f);
				MinBar.transform.localPosition = new Vector3 (-62.23f,-35.3f,-3.55f);
				PCState = 0;
			}
		} else {
			if (NowPointName == "PornMainPause") {
				PornPause.transform.localScale = new Vector3 (0.0f,0.0f,0.0f);
				PPlayer [0].GetComponent<UnityEngine.Video.VideoPlayer> ().Play ();
				PornStatePause = false;
				PCState = 1;
			} else if (NowPointName == "PronMin") {
				PornPause.transform.localScale = new Vector3 (0.0f,0.0f,0.0f);
				DeskTop.transform.localPosition= new Vector3(-176.0f,69.45f,520.0f);
				PornMainWindow.transform.localPosition = new Vector3 (-176.0f,67.099f,525.97f);
				MinBar.transform.localPosition = new Vector3 (-62.23f,-35.3f,-3.55f);
				PCState = 0;
			}

		}
	}

}
