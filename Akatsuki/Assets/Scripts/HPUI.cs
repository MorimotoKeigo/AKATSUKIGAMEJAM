using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour {

	private GameManager _gm;
	private Text Hptext;

	// Use this for initialization
	void Start () {
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		Hptext = this.GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		Hptext.text = _gm.PlayerHP + " / 100";

		if(_gm.PlayerHP <= 20) {
			Hptext.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
		} else {
			Hptext.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);			
		}
	}
}
