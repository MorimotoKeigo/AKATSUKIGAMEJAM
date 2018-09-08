using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UIStageCreater:MonoBehaviour{

	//Text読み込む
	public TextAsset textAsset;

	//配置するオブジェクト
	public GameObject floor_UI;
	public GameObject wall_UI;


	public GameObject Map_parent;
	//public GameObject player;


	//public Vector3 spaceScale = new Vector3(1.0f,1.0f,0f);

	//char[,] StageData = new char[10,10];
	//List<GameObject> StageList = new List<GameObject>();

	void Start () {

		// Already StageData
		GameObject[] stages = GameObject.FindGameObjectsWithTag("UI_Stage");
		foreach(GameObject stage in stages) {
			GameObject.DestroyImmediate(stage);
		}

		// var createPos = new Vector3 (-185, 170, 0);
		UI_CreateStage();
	}


	void UI_CreateStage(){
		/*
		pos.x += 5.0f;
		pos.z -= 5.0f;

		pos.y += 2.5f;
		*/

		// Vector3 originPos = pos;
		string stageTextData = textAsset.text;

		foreach(char c in stageTextData){

			GameObject obj = null;

			if(c == 'F'){
				obj = Instantiate(floor_UI, Map_parent.transform) as GameObject;
				obj.name = "UI_Floor";
				obj.tag = "UI_Stage";
				// obj.transform.parent = Map_parent.transform;
				// pos.x += 10.0f;
			} else if (c == '#') { // else(c == '#')
				obj = Instantiate(wall_UI, Map_parent.transform) as GameObject;
				obj.name = "UI_Wall";
				obj.tag = "UI_Stage";
				// obj.GetComponent <RectTransform>();
				// obj.transform.parent = Map_parent.transform;
				// pos.x += 10.0f;
			}
			/*else if(c == 'T'){
				//obj = Instantiate(tree, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
				//obj.name = "Tree";
				//obj.tag = "Stage";
				//pos.x += 10.0f;
			}
			*/
			/*
			else if(c == '\n'){
				// pos.z -= 10.0f;
				// pos.x = originPos.x;
			}else if(c == ' '){
				// pos.x += 10.0f;
			}
			*/

		}
	}

}
