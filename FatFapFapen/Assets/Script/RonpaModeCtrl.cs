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

	public GameObject Wheel;

	public int CurrentPhase = 0;
	private string[] QT ={"在幹嘛?為啥要鎖門?","你為啥不穿褲子?","剛剛那是什麼聲音?","剛剛那是什麼聲音?","你那是什麼聲音? ","沒事別一直用電腦阿?","休假日出去動動啦! ","阿親戚來你怎麼還窩在房間?","你那是..."};
    private string[] A1T ={"關你屁事","我想要通風一下","隔壁貓叫","電腦中毒啦!","OK，好","OK，好","他們來關我屁事","別人傳給我的啦"};
	private string[] A2T ={"就想一個人待著","褲子只是裝飾","我也不知道","Youtube廣告啦!","你是想殺死我嗎?","肥宅出門會死掉","歐，是喔，我不知道","我不知道電腦突然就"};
	private string[] A3T ={"我尻尻也要跟你說喔","想尻一下","是沒聽過片片喔?","配菜音樂阿!","我這是在工作啦!","不要","不然勒?要讓我過去曬肚肉嗎?","阿就片子阿，一起看嗎?"};
    private float[] A1D ={-0.1f,-0.25f,-0.15f,-0.05f,-0.1f,-0.2f,-0.1f,-0.1f};
    private float[] A2D ={-0.2f,-0.1f,-0.05f,-0.12f,-0.05f,-0.1f,-0.1f,-0.05f};
    private float[] A3D ={-0.25f,-0.3f,-0.3f,-0.3f,-0.15f,0.05f,-0.1f,-0.4f};
    private float[] A3S ={0.3f,0.4f,0.4f,0.4f,0,0,0,1};
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
