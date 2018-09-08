using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_setting : MonoBehaviour {

	public GameObject[] Light_prefabs;

	public GameObject[] Light_standing;


	public float x, y, z;
	public int type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K)) {
			Light_set_3d (x,y,z,type);
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			Light_remove_3d (type);
		}


	}


	public void Light_set_3d(float xx,float yy, float zz,int color_type){
		//color_type 1で色分け
		if (Light_standing [color_type] == null) {
			Light_standing [color_type] =
				Instantiate (Light_prefabs [color_type], new Vector3 (xx, yy, zz), Quaternion.identity);
		} else {
			Debug.Log ("設置済み");
		}

	}

	public void Light_remove_3d(int color_type){
		if (Light_standing [color_type] != null) {
			Destroy (Light_standing [color_type]);
			Light_standing [color_type] = null;
		} else {
			Debug.Log ("撤去済み");
		}


	}



}
