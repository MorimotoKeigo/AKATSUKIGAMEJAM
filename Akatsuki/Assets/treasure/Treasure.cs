using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {
	GameObject Player;
	public float distance_PT=200f;


	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		distance_PT=Vector3.Distance(this.gameObject.transform.position,Player.transform.position);
		if (Player.GetComponent<joyconSettingExample> ().shind_L_kyori>distance_PT) {
			Player.GetComponent<joyconSettingExample> ().shind_L_kyori = distance_PT;
			Player.GetComponent<joyconSettingExample> ().shindL_type = 0;
		}
		if (Player.GetComponent<joyconSettingExample> ().shindL_type == 1) {
			Player.GetComponent<joyconSettingExample> ().shind_L_kyori = distance_PT;
		}

		if (distance_PT < 5.0f) {
			Player.GetComponent<joyconSettingExample> ().shind_L_kyori = 50f;
			Player.GetComponent<joyconSettingExample> ().shindL_type = -1;
			Get_Treasure ();
		}
	}


	void Get_Treasure(){

		GameManager.instance.tresure_num--;

		//ここにGameEngineを呼び出す
		Destroy (this.gameObject);

	}


}
