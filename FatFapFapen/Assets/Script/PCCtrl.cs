using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PCCtrl: MonoBehaviour {

	public GameObject DeskTop;
	public GameObject PornMainWindow;
	public GameObject PornPause;
	public GameObject MinBar;
	public GameObject PPlayer;
	public Text CurrentMin;
	public Text CurrentSec;
	public Text TotalMin;
	public Text TotalSec;
	public Image Progressbar;

	public string NowPointName = "none";
	public int PCState = 0;// 0 DeskTop 1 Playing 2 Pause
	void SetCurrentTimeUI(){
		string minutes = Mathf.Floor ((int)PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().time / 60).ToString("00");
		string seconds = Mathf.Floor ((int)PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().time % 60).ToString("00");
		CurrentMin.text = minutes;
		CurrentSec.text = seconds;
	}
	void SetTotalTimeUI(){
		string minutes = Mathf.Floor ((int)PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().clip.length / 60).ToString("00");
		string seconds = Mathf.Floor ((int)PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().clip.length  % 60).ToString("00");
		TotalMin.text = minutes;
		TotalSec.text = seconds;
	}
	void SetProgressBar(){
		float Percentage = (float)(PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().time / PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().clip.length);
		Progressbar.fillAmount = Percentage;
	}

	// Use this for initialization
	void Start () {
		DeskTop.transform.localPosition= new Vector3(-176.0f,69.45f,520.0f);
		PornMainWindow.transform.localPosition = new Vector3 (-176.0f,67.099f,525.97f);
		MinBar.transform.localPosition = new Vector3 (-39.4f,-35.3f,2.69f);
		PornPause.transform.localScale = new Vector3 (0.0f,0.0f,0.0f);
		SetTotalTimeUI ();
		Progressbar.fillAmount = 0.0f;
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
		if (DataCtrl.Data.PornPause) {
			switch (PCState) {
			case 2:
				PornPause.transform.localScale = new Vector3 (-15.0f, 1.0f, 8.0f);
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Pause ();
				PCState = 1;
				NowPointName = null;
				break;
			case 4:
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Pause ();
				PCState = 3;
				NowPointName = null;
				break;
			}
		}
		if (PCState == 0) {
			if (NowPointName == "PornIcon") {
				PornPause.transform.localScale = new Vector3 (-15.0f, 1.0f, 8.0f);
				DeskTop.transform.localPosition = new Vector3 (-176.0f, 69.45f, 529.8f);
				MinBar.transform.localPosition = new Vector3 (-39.4f, -35.3f, 2.69f);
				PornMainWindow.transform.localPosition = new Vector3 (0.0f, 2.36f, 0.441f);
				DataCtrl.Data.PornPause = true;
				PCState = 1;
				NowPointName = null;
			}
		} else if (PCState == 1) {
			DataCtrl.Data.ChkScreen = true;
			if (NowPointName == "PornMainPause") {
				PornPause.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Play ();
				DataCtrl.Data.PornPause = false;
				PCState = 2;
				NowPointName = null;
			} else if (NowPointName == "PronMin") {
				PornPause.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
				DeskTop.transform.localPosition = new Vector3 (-176.0f, 69.45f, 520.0f);
				PornMainWindow.transform.localPosition = new Vector3 (-176.0f, 67.099f, 525.97f);
				MinBar.transform.localPosition = new Vector3 (-39.4f, -35.3f, -3.82f);
				PCState = 3;
				NowPointName = null;
			} else if (NowPointName == "PronOff") {
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Stop ();
				DeskTop.transform.localPosition= new Vector3(-176.0f,69.45f,520.0f);
				PornMainWindow.transform.localPosition = new Vector3 (-176.0f,67.099f,525.97f);
				MinBar.transform.localPosition = new Vector3 (-39.4f,-35.3f,2.69f);
				PornPause.transform.localScale = new Vector3 (0.0f,0.0f,0.0f);
				PCState = 0;
				NowPointName = null;
			}

		} else if (PCState == 2) {
			DataCtrl.Data.ChkScreen = true;
			if (NowPointName == "PornMain") {
				PornPause.transform.localScale = new Vector3 (-15.0f, 1.0f, 8.0f);
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Pause ();
				DataCtrl.Data.PornPause = true;
				PCState = 1;
				NowPointName = null;
			} else if (NowPointName == "PronMin") {
				PornPause.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
				DeskTop.transform.localPosition = new Vector3 (-176.0f, 69.45f, 520.0f);
				PornMainWindow.transform.localPosition = new Vector3 (-176.0f, 67.099f, 525.97f);
				MinBar.transform.localPosition = new Vector3 (-39.4f, -35.3f, -3.82f);
				PCState = 4;
				NowPointName = null;
			}else if (NowPointName == "PronOff") {
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Stop ();
				DeskTop.transform.localPosition= new Vector3(-176.0f,69.45f,520.0f);
				PornMainWindow.transform.localPosition = new Vector3 (-176.0f,67.099f,525.97f);
				MinBar.transform.localPosition = new Vector3 (-39.4f,-35.3f,2.69f);
				PornPause.transform.localScale = new Vector3 (0.0f,0.0f,0.0f);
				PCState = 0;
				NowPointName = null;
			}

		} else if (PCState == 3) {
			DataCtrl.Data.ChkScreen = false;
			if (NowPointName == "PornMinBar") {
				PornPause.transform.localScale = new Vector3 (-15.0f, 1.0f, 8.0f);
				DeskTop.transform.localPosition = new Vector3 (-176.0f, 69.45f, 529.8f);
				MinBar.transform.localPosition = new Vector3 (-39.4f, -35.3f, 2.69f);
				PornMainWindow.transform.localPosition = new Vector3 (0.0f, 2.36f, 0.441f);
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Pause ();
				DataCtrl.Data.PornPause = true;
				PCState = 1;
				NowPointName = null;
			}
		} else if (PCState==4){
			DataCtrl.Data.ChkScreen = false;
			if (NowPointName == "PornMinBar") {
				PornPause.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
				DeskTop.transform.localPosition = new Vector3 (-176.0f, 69.45f, 529.8f);
				PornMainWindow.transform.localPosition = new Vector3 (0.0f, 2.36f, 0.441f);
				MinBar.transform.localPosition = new Vector3 (-39.4f, -35.3f, 2.69f);
				PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().Play ();
				DataCtrl.Data.PornPause = false;
				PCState = 2;
				NowPointName = null;
			}
		}
		if (PPlayer.GetComponent<UnityEngine.Video.VideoPlayer> ().isPlaying) {
			SetCurrentTimeUI ();
			SetProgressBar ();
		}

	}

}
