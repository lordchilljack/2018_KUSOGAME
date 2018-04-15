using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ending_ctrl : MonoBehaviour {
    public TitleCtrl tc;
	// Use this for initialization
	void Start () {

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
