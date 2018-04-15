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
	public int CurrentGameMode = 0;
	public int Assult_Rate = 5;

	void EndMainGame(VideoPlayer VP){
		SceneManager.LoadScene(3);
	}

	// Use this for initialization
	void Start () {
		PornPlayer.GetComponent<VideoPlayer> ().loopPointReached += EndMainGame;
		CurrentGameMode = 0;
		DataCtrl.Data.GameMode = CurrentGameMode;
	}

	void ChangeGameMode(int seed){
		switch (seed) {

		case 0:
			NormalUI.enabled = true;
			RonpaMode.enabled = false;
			PlayerH.GetComponent<TurnHorizontal> ().enabled = true;
			PlayerV.GetComponent<TurnVertical> ().enabled = true;
			NormalUI.GetComponent<AudioSource> ().Play ();
			RonpaMode.GetComponent<AudioSource> ().Pause ();
			break;
		case 1:
			
			NormalUI.enabled = false;
			RonpaMode.enabled = true;
			PlayerH.transform.LookAt (Door.transform);
			PlayerH.GetComponent<TurnHorizontal>().enabled = false;
			PlayerV.GetComponent<TurnVertical>().enabled = false;
			NormalUI.GetComponent<AudioSource> ().Pause ();
			RonpaMode.GetComponent<AudioSource> ().Play ();
			break;
		case 2:
			if (DataCtrl.Data.HowEnd == 0) {
				SceneManager.LoadScene (3);
			} else if(DataCtrl.Data.HowEnd == 1){
				SceneManager.LoadScene (4);
			}
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		int assult_dice = Random.Range (0,100);


		//if (CurrentGameMode == 0) {
		//	if (assult_dice == Assult_Rate) {
		//		DataCtrl.Data.GameMode = 1;
		//	}
		//}
		CurrentGameMode = DataCtrl.Data.GameMode;
		ChangeGameMode (CurrentGameMode);
	}
}
