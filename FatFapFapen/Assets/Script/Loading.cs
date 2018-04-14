using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    public Image Loading_Bar;
    public float progress = 0;
    public int Foo = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        progress += Time.deltaTime*Random.Range(5,20);
        Loading_Bar.fillAmount = progress / 100;
        if(Foo == 0 && Loading_Bar.fillAmount > 0.7)
        {
            progress = 40;
            Foo = 1;
        }
        else if (Foo == 1 && Loading_Bar.fillAmount > 0.95)
        {
            progress = 75;
            Foo = 2;
        }
        else if (Foo == 2 && Loading_Bar.fillAmount >= 1)
        {
            SceneManager.LoadScene(2);
        }
        //print(Loading_Bar.fillAmount);
    }
}
