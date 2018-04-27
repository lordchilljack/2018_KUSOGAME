using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ending_ctrl : MonoBehaviour {
    public TitleCtrl tc;
	public Text Scorce;
	public Image e04;
	// Use this for initialization
	void Start () {
		
		Scorce.text = DataCtrl.Data.scorce.ToString();
		e04.transform.localScale=new Vector2 (0.0f,0.0f);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void menuGame()
    {
		DataCtrl.Data = null;
        SceneManager.LoadScene(0);
    }

    public void ReGame()
    {
		int nowtime = (int)Time.deltaTime;
		int newtime =nowtime;
		DataCtrl.Data = null;
        SceneManager.LoadScene(2);
    }

}
