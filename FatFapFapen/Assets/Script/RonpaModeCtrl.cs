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

	public Image OtakuFace;

	public int CurrentPhase = 0; //論破模式進行階段
	public float TinkTimeLimit = 15.0f; //思考時間極限
	public float TinkTimeTimer = 0.0f; //思考時間計數器
	public bool Answered = false; //首否已送出回答
	public float D_Percent = 0.0f; //危險度百分比
	public float S_Percent = 0.0f; //害羞度百分比
	public int CurrentAws =0;
	public int[] Questions_Order;
	public int Question_Statue;

	private string[] QT ={"在幹嘛?為啥要鎖門?","你為啥不穿褲子?","剛剛那是什麼聲音?","你那是什麼聲音? ","沒事別一直用電腦阿?","休假日出去動動啦! ","阿親戚來你怎麼還窩在房間?","你那是..."};
    private string[] A1T ={"關你屁事","我想要通風一下","隔壁貓叫","電腦中毒啦!","OK，好","OK，好","他們來關我屁事","別人傳給我的啦"};
	private string[] A2T ={"就想一個人待著","褲子只是裝飾","我也不知道","Youtube廣告啦!","你是想殺死我嗎?","肥宅出門會死掉","歐，是喔，我不知道","我不知道電腦突然就"};
	private string[] A3T ={"我尻尻也要跟你說喔","想尻一下","是沒聽過片片喔?","配菜音樂阿!","我這是在工作啦!","不要","不然勒?要讓我過去曬肚肉嗎?","阿就片子阿，一起看嗎?"};
    private float[] A1D ={-0.1f,-0.25f,-0.15f,-0.05f,-0.1f,-0.2f,0.2f,-0.1f};
    private float[] A2D ={-0.2f,-0.1f,-0.05f,-0.12f,0.05f,-0.1f,-0.1f,-0.05f};
    private float[] A3D ={-0.25f,-0.3f,-0.3f,0.1f,-0.15f,0.05f,-0.1f,-0.4f};
    private float[] A3S ={0.3f,0.4f,0.4f,0.4f,0.0f,0.0f,0.0f,1.0f};

	private Sprite d_1;
	private Sprite d_2;
	private Sprite d_3;
	private Sprite h_1;
	private Sprite h_2;
	private Sprite h_3;
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
		float FinalDanger = 0.01f;
		if (DataCtrl.Data.Chkpants)
			FinalDanger += 0.33f;
		if (DataCtrl.Data.ChkVolume)
			FinalDanger += 0.25f;
		if (DataCtrl.Data.ChkScreen)
			FinalDanger += 0.33f;
		GameObject TP = GameObject.Find ("ToiletPaper(Clone)");
		if (TP)
			FinalDanger += 0.1f;
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
		TinkTimeTimer = 0.0f;
		Answered = false;
		Aws1.GetComponentInParent<Button>().enabled = true;
		Aws2.GetComponentInParent<Button>().enabled = true;
		Aws3.GetComponentInParent<Button>().enabled = true;
	}

	//
	//算出答案，同時鎖定其他按鈕
	//
	public void sendAws(string BtName){
		if (BtName == "A1") {
			D_Percent += A1D [Questions_Order[CurrentPhase]];
			Aws2.GetComponentInParent<Button>().enabled = false;
			Aws3.GetComponentInParent<Button>().enabled = false;
			CurrentAws = 1;
			Answered = true;
		} else if (BtName == "A2") {
			D_Percent += A2D [Questions_Order[CurrentPhase]];
			Aws1.GetComponentInParent<Button>().enabled = false;
			Aws3.GetComponentInParent<Button>().enabled = false;
			CurrentAws = 2;
			Answered = true;
		} else if (BtName == "A3") {
			if (S_Percent <= 0.99f) {
				D_Percent += A3D [Questions_Order [CurrentPhase]];
				S_Percent += A3S [Questions_Order [CurrentPhase]];
				Aws1.GetComponentInParent<Button> ().enabled = false;
				Aws2.GetComponentInParent<Button> ().enabled = false;
				CurrentAws = 3;
				Answered = true;
			} else
				Aws3.text = "太羞恥啦我說不出口";
		}
	}

	// Use this for initialization
	void Start () {
		Answered = false;
		CurrentPhase = 0;
		FillingQA (0);
		EveryRoundSetUp (0);
		Questions_Order = new int[5]; // Question Order
		Questions_Order[0] = 0;
		S_Percent = DataCtrl.Data.S_Gauge;
		D_Percent = CheckDangerPercent()+DataCtrl.Data.D_Gauge;
		if (D_Percent >= 1.0f)
			D_Percent = 1.0f;
		DGague.fillAmount = D_Percent;
		SGauge.fillAmount = S_Percent;
		for (int i = 1; i < Questions_Order.Length; i++) {
			Questions_Order [i] = Random.Range (1, 7);
			for (int j = 1; j < i; j++) {
				while (Questions_Order [i] == Questions_Order [j]) {
					j = 1;
					Questions_Order [i] = Random.Range (1, 7);
				}
			}
		}
		d_1 = Resources.Load<Sprite> ("d_1");
		d_2 = Resources.Load<Sprite> ("d_2");
		d_3 = Resources.Load<Sprite> ("d_3");
		h_1 = Resources.Load<Sprite> ("h_1");
		h_2 = Resources.Load<Sprite> ("h_2");
		h_3 = Resources.Load<Sprite> ("h_3");

	}
	
	// Update is called once per frame
	void Update () {
		if (D_Percent <= 0.0f) {
			D_Percent = 0.0f;
		}
		if (S_Percent >= 1.0f) {
			S_Percent = 1.0f;
		}
		if (D_Percent <= 0.3f) {
			if (S_Percent < 0.3f) {
				OtakuFace.sprite = d_1;
			} else {
				OtakuFace.sprite = h_1;
			}
		} else if (D_Percent > 0.33f&& D_Percent<=0.66f) {
			if (S_Percent < 0.3f) {
				OtakuFace.sprite = d_2;
			} else {
				OtakuFace.sprite = h_2;
			}
		} else{
			if (S_Percent < 0.3f) {
				OtakuFace.sprite = d_3;
			} else {
				OtakuFace.sprite = h_3;
			}
		}
		DGague.fillAmount = D_Percent;
		SGauge.fillAmount = S_Percent;
		TinkTimeTimer += Time.deltaTime;
		TinkingTime.text = (TinkTimeLimit - TinkTimeTimer).ToString("0.00");
		if (CurrentPhase < 5 && D_Percent>=0.01f) {
			if (TinkTimeTimer >= TinkTimeLimit || Answered) {
				CurrentPhase++;
				if((CurrentPhase < 5)){
					FillingQA (Questions_Order [CurrentPhase]);
					EveryRoundSetUp (CurrentPhase);
				}

			}
		} else {
			//end RonpaMode 
			if (D_Percent>= 0.8f) {
				DataCtrl.Data.HowEnd = 1;
				DataCtrl.Data.GameMode = 2;
				DataCtrl.Data.NeedChange = true;
				Start ();
				gameObject.GetComponent<RonpaModeCtrl> ().enabled = false;
			} else {
				DataCtrl.Data.GameMode = 0;
				DataCtrl.Data.NeedChange = true;
				DataCtrl.Data.D_Gauge = D_Percent;
				DataCtrl.Data.S_Gauge = S_Percent;
				Start ();
				gameObject.GetComponent<RonpaModeCtrl> ().enabled = false;
			}
		}

	}
}
