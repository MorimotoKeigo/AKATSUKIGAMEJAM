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

		if (distance_PT < 5.0f) {
			Get_Treasure ();
		}
	}


	void Get_Treasure(){


		//ここにGameEngineを呼び出す
		Destroy (this.gameObject);

	}


}
