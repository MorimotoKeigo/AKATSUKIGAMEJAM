using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour {

	private GameManager _gm;
	public Text time_text;

	// Use this for initialization
	void Start () {
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		// time_text.text = _gm.remainTime.toString() + "秒残してクリア！";
	}

	// Update is called once per frame
	void Update () {

	}
}
