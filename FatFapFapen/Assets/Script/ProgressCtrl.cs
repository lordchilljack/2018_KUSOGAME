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

	void EndMainGame(VideoPlayer VP){
		SceneManager.LoadScene(3);
	}

	// Use this for initialization
	void Start () {
		PornPlayer.GetComponent<VideoPlayer> ().loopPointReached += EndMainGame;
	}

	void ChangeGameMode(int seed){
		switch (seed) {

		case 0:
			NormalUI.enabled = true;
			RonpaMode.enabled = false;
			PlayerH.GetComponent<TurnHorizontal>().enabled = true;
			PlayerV.GetComponent<TurnVertical>().enabled = true;
			break;
		case 1:
			
			NormalUI.enabled = false;
			RonpaMode.enabled = true;
			PlayerH.transform.LookAt (Door.transform);
			PlayerH.GetComponent<TurnHorizontal>().enabled = false;
			PlayerV.GetComponent<TurnVertical>().enabled = false;
			break;
		case 2:
			SceneManager.LoadScene(3);
			break;
		}

	}

	// Update is called once per frame
	void Update () {
		ChangeGameMode (CurrentGameMode);
	}
}
