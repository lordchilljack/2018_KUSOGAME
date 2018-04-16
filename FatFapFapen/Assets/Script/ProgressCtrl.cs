using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ProgressCtrl : MonoBehaviour {
	public Canvas NormalUI;
	public Canvas RonpaMode;
	public GameObject PlayerH;
	public GameObject PlayerV;
	public GameObject Door;
	public GameObject PornPlayer;
	public int Assult_Rate = 30;
	public int Assult_dice = 0;
	public float TimeAnchorOld = 0.0f;
	public float TimeAnchorNew = 0.0f;
	public float TimeLowBound;
	public float TimeHiiBound;
	public float TimeRadBound;
	void EndMainGame(VideoPlayer VP){
		SceneManager.LoadScene(3);
	}

	void ChangeGameMode(int seed){
		switch (seed) {

		case 0:
			NormalUI.enabled = true;
			RonpaMode.enabled = false;
			PlayerH.GetComponent<TurnHorizontal> ().enabled = true;
			PlayerV.GetComponent<TurnVertical> ().enabled = true;
			if (!NormalUI.GetComponent<AudioSource> ().isPlaying) {
				NormalUI.GetComponent<AudioSource> ().Play ();
			}
			if (RonpaMode.GetComponent<AudioSource> ().isPlaying) {
				RonpaMode.GetComponent<AudioSource> ().Pause ();
			}
			DataCtrl.Data.NeedChange = false;
			break;
		case 1:
			NormalUI.enabled = false;
			RonpaMode.enabled = true;
			PlayerH.transform.LookAt (Door.transform);
			PlayerH.GetComponent<TurnHorizontal> ().enabled = false;
			PlayerV.GetComponent<TurnVertical> ().enabled = false;
			RonpaMode.GetComponent<RonpaModeCtrl> ().enabled = true;
			//PornPlayer.GetComponent<VideoPlayer> ().Pause ();
			DataCtrl.Data.PornPause = true;
			if (NormalUI.GetComponent<AudioSource> ().isPlaying) {
				NormalUI.GetComponent<AudioSource> ().Pause ();
			}
			if (!RonpaMode.GetComponent<AudioSource> ().isPlaying) {
				RonpaMode.GetComponent<AudioSource> ().Play ();
			}
			DataCtrl.Data.NeedChange = false;
			break;
		case 2:
			if (DataCtrl.Data.HowEnd == 0) {
				SceneManager.LoadScene (3);
			} else if(DataCtrl.Data.HowEnd == 1){
				SceneManager.LoadScene (4);
			}
			DataCtrl.Data.NeedChange = false;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		PornPlayer.GetComponent<VideoPlayer> ().loopPointReached += EndMainGame;
		DataCtrl.Data.GameMode = 0;
		TimeAnchorOld += Time.deltaTime;
		ChangeGameMode (DataCtrl.Data.GameMode);
		TimeRadBound = Random.Range (TimeLowBound,TimeHiiBound);
	}



	// Update is called once per frame
	void Update () {
		if (DataCtrl.Data.GameMode == 0) {
			Assult_dice = Random.Range (0,100);
			TimeAnchorNew += Time.deltaTime;
			if ((TimeAnchorNew - TimeAnchorOld) >= TimeRadBound && (TimeAnchorNew - TimeAnchorOld) < TimeRadBound+1) {
				if (Assult_dice <= Assult_Rate) {
					if (!gameObject.GetComponent<AudioSource> ().isPlaying) {
						gameObject.GetComponent<AudioSource> ().Play ();
					}	
				}
			} else if((TimeAnchorNew - TimeAnchorOld) >= TimeRadBound+5 && (TimeAnchorNew - TimeAnchorOld) < TimeRadBound+6){
				gameObject.GetComponent<AudioSource> ().Pause ();
				TimeRadBound = Random.Range (TimeLowBound,TimeHiiBound);
				if (Assult_dice <= Assult_Rate) {
					DataCtrl.Data.GameMode = 1;
					DataCtrl.Data.NeedChange = true;
				}
				TimeAnchorNew = 0;
				TimeAnchorOld = 0;
			}
		} else if (DataCtrl.Data.GameMode == 1) {
			TimeAnchorNew = 0;
			TimeAnchorOld = 0;
		}

		if (DataCtrl.Data.NeedChange == true) {
			ChangeGameMode (DataCtrl.Data.GameMode);
		}
	}
}
