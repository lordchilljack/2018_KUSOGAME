using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RonpaModeCtrl : MonoBehaviour {
	public Text QuestionContent;
	public Text Aws1;
	public Text Aws2;
	public Text Aws3;

	public Text QuestionNum;
	public Text TinkingTime;

	public Image Wheel;
	public Image DGague;
	public Image SGauge;

	public int CurrentPhase = 0; //論破模式進行階段
	public float TinkTimeLimit = 10.0f; //思考時間極限
	public float TinkTimeTimer = 0.0f; //思考時間計數器
	public bool Answered = false; //首否已送出回答
	public float D_Percent = 0.0f; //危險度百分比
	public float S_Percent = 0.0f; //害羞度百分比
	public int CurrentAws =0;

	private string[] QT ={"在幹嘛?為啥要鎖門?","你為啥不穿褲子?","剛剛那是什麼聲音?","剛剛那是什麼聲音?","你那是什麼聲音? ","沒事別一直用電腦阿?","休假日出去動動啦! ","阿親戚來你怎麼還窩在房間?","你那是..."};
    private string[] A1T ={"關你屁事","我想要通風一下","隔壁貓叫","電腦中毒啦!","OK，好","OK，好","他們來關我屁事","別人傳給我的啦"};
	private string[] A2T ={"就想一個人待著","褲子只是裝飾","我也不知道","Youtube廣告啦!","你是想殺死我嗎?","肥宅出門會死掉","歐，是喔，我不知道","我不知道電腦突然就"};
	private string[] A3T ={"我尻尻也要跟你說喔","想尻一下","是沒聽過片片喔?","配菜音樂阿!","我這是在工作啦!","不要","不然勒?要讓我過去曬肚肉嗎?","阿就片子阿，一起看嗎?"};
    private float[] A1D ={-0.1f,-0.25f,-0.15f,-0.05f,-0.1f,-0.2f,-0.1f,-0.1f};
    private float[] A2D ={-0.2f,-0.1f,-0.05f,-0.12f,-0.05f,-0.1f,-0.1f,-0.05f};
    private float[] A3D ={-0.25f,-0.3f,-0.3f,-0.3f,-0.15f,0.05f,-0.1f,-0.4f};
    private float[] A3S ={0.3f,0.4f,0.4f,0.4f,0,0,0,1};

	//
	// 自動填入相對映問題與回答
	//
	void FillingQA(int QAPattern){
		QuestionContent.text = QT[QAPattern];
		Aws1.text = A1T[QAPattern];
		Aws2.text = A2T[QAPattern];
		Aws3.text = A3T[QAPattern];	
		//TODO:加動畫
	}

	//
	// 檢查環境中 褲子是否有穿 是否最小化 是否有片子聲音 一項 +0.33 危險度
	//
	float CheckDangerPercent(){
		float FinalDanger=0;
		/*
		if(褲鍊開啟){
			FinalDanger=FinalDanger+0.25f;
		}
		if(!影片最小化){
			FinalDanger=FinalDanger+0.30f;
		}
		if(影片有聲音&&播放中){
			FinalDanger=FinalDanger+0.25f;
		}

		if(GameObject.Find("ToiletPaper(Clone)")!=NULL){
			FinalDanger=FinalDanger+0.33f;
		}
		*/

		return FinalDanger;
	}

	//
	// 顯示第幾問 並初始 思考時間計時器顯示
	//
	void EveryRoundSetUp(int Nowis){
		switch (Nowis) {

		case 0:
			QuestionNum.text = "1st";
			break;
		case 1:
			QuestionNum.text = "2nd";
			break;
		case 2:
			QuestionNum.text = "3rd";
			break;
		case 3:
			QuestionNum.text = "4th";
			break;
		case 4:
			QuestionNum.text = "Last";
			break;
		}
		TinkingTime.text = TinkTimeLimit.ToString();
	}

	//
	//算出答案，同時鎖定其他按鈕
	//
	public void sendAws(string BtName){
		if (BtName == "A1") {
			Aws2.GetComponentInParent<Button>().enabled = false;
			Aws3.GetComponentInParent<Button>().enabled = false;
			CurrentAws = 1;
		} else if (BtName == "A2") {
			Aws1.GetComponentInParent<Button>().enabled = false;
			Aws3.GetComponentInParent<Button>().enabled = false;
			CurrentAws = 2;
		} else if (BtName == "A3") {
			Aws1.GetComponentInParent<Button>().enabled = false;
			Aws2.GetComponentInParent<Button>().enabled = false;
			CurrentAws = 3;
		}
	}

	// Use this for initialization
	void Start () {
		FillingQA (0);
		EveryRoundSetUp (0);
	}
	
	// Update is called once per frame
	void Update () {

		if(TinkTimeTimer==TinkTimeLimit||Answered){
			//跳下一題
		}


	}
}
