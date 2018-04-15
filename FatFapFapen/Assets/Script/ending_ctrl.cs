using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ending_ctrl : MonoBehaviour {
    public TitleCtrl tc;
	public Text Scorce;
	// Use this for initialization
	void Start () {
		Scorce.text = GameDataManager.scorce.ToString();
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
        SceneManager.LoadScene(0);
    }

    public void ReGame()
    {
        SceneManager.LoadScene(2);
    }

}
