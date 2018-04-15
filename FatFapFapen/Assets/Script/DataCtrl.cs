using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCtrl : MonoBehaviour {
	public static GameDataManager _Data;
	public static GameDataManager Data{
		get{
			if (_Data == null) {
				_Data = new GameDataManager ();
			}
			return _Data;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
