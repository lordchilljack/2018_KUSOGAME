using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour {
	public GameObject PlayerH;
	public GameObject PlayerV;
	public AudioSource PornAudio;
	public GameObject PornVideo;
	public Canvas HUDDisplay;
	public int VolumeStatus = 5;
	public Text CurrentVolume = null;
	public float ListenCarefullyCountdown = 0;
	public Text CurrentHearing = null;
	public float HearingTelent = 0;
	public string JizzStatus = "off";
	public float JizzDefaultTime = 5;
	public float JizzTelent = 0;
	public float JizzCurrentTime = 0;
	public Image JizzBar;
	public string SaintStatus = "off";
	public float SaintDefaultTime = 30;
	public float SaintTelent = 0;
	public float SaintCountdown = 0;
	public Text CurrentSaint = null;
	public Text CurrentNotice = null;
    public int JP = 0;
    public int Score = 0;
    public float Gain = 1;
	public string NoticeInfo;
	public float NoticeTimer = 2;
	private int NowRH=0;
	private int NowRV=0;
	private bool PantsWaer = true;
 
	// Use this for initialization
	void Start () {
		PornAudio.volume = (float)VolumeStatus/10;		
	}

	//
	// 回傳玩家目前面對的垂直狀況 0 正面 1 下面 2 上面
	//
	int PlayerFacingV(){
		float NowFacing;
		NowFacing = PlayerV.transform.rotation.eulerAngles.x;
		//Debug.Log ("NowFacing "+PlayerV.transform.rotation.eulerAngles.x.ToString());
		if ((NowFacing < 25.0f && NowFacing > 0.0f)|| (NowFacing > 320.0f && NowFacing <= 360.0f)) {
			return 0;
		} else if (NowFacing <= 50.0f && NowFacing > 25.0f) {
			return 1;
		} else if(NowFacing < 320.0f && NowFacing >= 310.0f){
			return 2;
		} else{
			return 0;
		}
	}

	//
	// 回傳玩家目前面對的水平狀況 0 正面 1 左 2 右 3 後
	//
	int PlayerFacingH(){
		float NowFacing;
		NowFacing = PlayerH.transform.rotation.eulerAngles.y;
		//Debug.Log ("NowFacing "+PlayerH.transform.rotation.eulerAngles.y.ToString());
		if ((NowFacing <= 70.0f && NowFacing > 0.0f) || (NowFacing <= 360.0f && NowFacing > 290.0f)) {
			return 0;
		} else if (NowFacing <= 290.0f && NowFacing > 260.0f) {
			return 1;
		} else if (NowFacing <= 100.0f && NowFacing > 70.0f) {
			return 2;
		} else if (NowFacing <= 260.0f && NowFacing > 100.0f) {
			return 3;
		} else {
			return 0;
		}
	}

    //
    // 音量調整
    //
    public void RefeshVolumeStatus(string VolumeCtrl) {
        if (VolumeCtrl == "up") {
            if (VolumeStatus < 10) {
                VolumeStatus++;
                Gain += VolumeStatus * 0.1f;
            }
        } else if (VolumeCtrl == "down") {
            if (VolumeStatus > 0) {
                VolumeStatus--;
                Gain -= VolumeStatus * 0.1f;
            }
        }
        if (VolumeStatus == 0) {
            CurrentVolume.text = "MUTE";
            Gain = 1;
        }
        else if (VolumeStatus == 10) {
            CurrentVolume.text = "MAX";
            Gain = 2;
        }
        else
            CurrentVolume.text = "".PadRight(VolumeStatus, '♦');
		PornAudio.volume = (float)VolumeStatus/10;
	}

	//
	// 仔細聆聽
	//
	public void ListenCarefully(){
		if(ListenCarefullyCountdown == 0.0)
			ListenCarefullyCountdown = 10.0f + HearingTelent;
	}	

	//
	// 消耗衛生紙
	//
	public void JizzProcess(){
		NoticeInfo = "on";
		if (SaintStatus == "off" && !PantsWaer) {
			JizzStatus = "on";
		} else if(SaintStatus == "off" && PantsWaer){
			CurrentNotice.text = "褲鏈未開啟";
		} else if(SaintStatus == "on"){
			CurrentNotice.text = "賢者模式中";
		}
	}

	//
	// 拉開/關起褲鏈
	//
	public void ZipzapPants(){
		NoticeInfo = "on";
		if (JizzCurrentTime != 0) {
			PantsWaer = false;
			CurrentNotice.text = "高潮中 制御不可";
		} else {
			PantsWaer = !PantsWaer;
			if (PantsWaer) {
				CurrentNotice.text = "褲鏈已拉上";
			} else {
				CurrentNotice.text = "褲鏈解放中";
			}
		}

	}

	//
	//依據玩家視角影藏HUD
	//
	void HUDHide(){
		NowRH = PlayerFacingH ();
		NowRV = PlayerFacingV ();
		GameObject[] FrontButtons = GameObject.FindGameObjectsWithTag ("FrontAct");
		GameObject[] LowButtons = GameObject.FindGameObjectsWithTag ("LowAct");
		if (NowRH == 0) {
			if (NowRV == 0) {
				for (int i = 0; i < FrontButtons.Length; i++) {
					FrontButtons [i].transform.localScale = new Vector3 (1, 1, 1);
				}
			} else {
				for(int i=0;i<FrontButtons.Length;i++){
					FrontButtons [i].transform.localScale=new Vector3(0, 0, 0);
				}
			}
		} else{
			for(int i=0;i<FrontButtons.Length;i++){
				FrontButtons [i].transform.localScale=new Vector3(0, 0, 0);
			}
		}
		if (NowRV == 1) {
			LowButtons [0].transform.localScale = new Vector3 (1, 1, 1);
		} else {
			LowButtons [0].transform.localScale = new Vector3 (0, 0, 0);
		}
	}
	//	
	// UI連動更新
	//
	void HUDUpdate(){
		//print (ListenCarefullyCountdown);
		if (NoticeInfo == "on") {
			NoticeTimer -= Time.deltaTime;
			if (NoticeTimer < 0) {
				CurrentNotice.text = "";
				NoticeTimer = 2;
				NoticeInfo = "off";
			}
		}
		if (JizzStatus == "on") {
			JizzCurrentTime += Time.deltaTime;
			JizzBar.fillAmount = JizzCurrentTime / (JizzDefaultTime - JizzTelent);
			//print (JizzBar.fillAmount);
			if (JizzCurrentTime >= (JizzDefaultTime - JizzTelent)) {
				JizzStatus = "off";
				JizzCurrentTime = 0;
				SaintStatus = "on";
				JizzBar.fillAmount = 0;
                Score += JP;
			}
		}
		if (SaintStatus == "on") {
			if (SaintCountdown == 0) {
				SaintCountdown = SaintDefaultTime - SaintTelent;
			} else {
				SaintCountdown -= Time.deltaTime;
				if (SaintCountdown <= 0) {
					SaintStatus = "off";
					SaintCountdown = 0;
					CurrentSaint.text = "尻尻已就緒";
				} else {
					CurrentSaint.text = "".PadRight ((int)Mathf.Floor (SaintCountdown), '♦');
				}
			}
		}
		if (ListenCarefullyCountdown > 0) {
			ListenCarefullyCountdown -= Time.deltaTime;
			if (ListenCarefullyCountdown > 0.0)
				CurrentHearing.text = "".PadRight ((int)Mathf.Floor(ListenCarefullyCountdown), '♦');
			else{
				ListenCarefullyCountdown = 0;
				CurrentHearing.text = "未啟動";}
		} else if (ListenCarefullyCountdown <= 0) {
			ListenCarefullyCountdown = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		

			;
		HUDHide ();
		HUDUpdate ();
	}

}
