using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour {
	public GameObject Player;
	public Canvas HUDDisplay;
	public int VolumeStatus = 0;
	public Text CurrentVolume = null;
	public int H;
	public int V;

	// Use this for initialization
	void Start () {
		
	}

	//
	// 回傳玩家目前面對的垂直狀況 0 正面 1 下面 2 上面
	//
	int PlayerFacingV(){
		float NowFacing;
		NowFacing = Player.GetComponentInChildren<GameObject>().transform.localRotation.x;
		if (NowFacing <= 30.0f && NowFacing > -39.0f) {
			return 0;
		} else if (NowFacing <= -39.0f) {
			return 1;
		} else{
			return 2;
		}
	}

	//
	// 回傳玩家目前面對的水平狀況 0 正面 1 左 2 右 3 後
	//
	int PlayerFacingH(){
		float NowFacing;
		NowFacing = Player.transform.localRotation.y;
		if (NowFacing <= 60.0f && NowFacing > -60.0f) {
			return 0;
		} else if (NowFacing <= -60.0f && NowFacing > -100.0f) {
			return 1;
		} else if (NowFacing <= 100.0f && NowFacing > 60.0f) {
			return 2;
		} else{
			return 3;
		}
	}

	//
	// 音量調整
	//
	public void RefeshVolumeStatus(string VolumeCtrl){
		if (VolumeCtrl == "up") {
			if (VolumeStatus < 10) {
				VolumeStatus++;
			}
		} else if (VolumeCtrl == "down") {
			if (VolumeStatus > 0) {
				VolumeStatus--;
			}
		}
		if (VolumeStatus == 0)
			CurrentVolume.text = "MUTE";
		else if (VolumeStatus == 10)
			CurrentVolume.text = "MAX";
		else
			CurrentVolume.text = "".PadRight(VolumeStatus,'♦');
	}

	//
	//調整HUD是否要顯示
	//
	void HUDUpdate(){

	}
	// Update is called once per frame
	void Update () {

	}
}
