using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class StageCreater:MonoBehaviour{

	//Text読み込む
	public TextAsset textAsset;

	//配置するオブジェクト
	public GameObject floor;
	public GameObject wall;


	public GameObject stage;
	//public GameObject player;

	public Vector3 createPos;


	//public Vector3 spaceScale = new Vector3(1.0f,1.0f,0f);

	//char[,] StageData = new char[10,10];
	//List<GameObject> StageList = new List<GameObject>(); 

	void Start () {

		// Already StageData
		GameObject[] stages = GameObject.FindGameObjectsWithTag("Stage");
		foreach(GameObject stage in stages) {
			GameObject.DestroyImmediate(stage);
		}

		createPos = Vector3.zero;
		CreateStage(createPos);

	}


	void CreateStage(Vector3 pos){

		pos.x += 5.0f;
		pos.z -= 5.0f;

		pos.y += 2.5f;

		Vector3 originPos = pos;
		string stageTextData = textAsset.text;

		foreach(char c in stageTextData){

			GameObject obj = null;

			if(c == 'F'){
				obj = Instantiate(floor, pos, Quaternion.Euler(0, 90, 0)) as GameObject;
				obj.name = "FLOOR";
				obj.tag = "Stage";
				obj.transform.parent = stage.transform;
				pos.x += 10.0f;
			}else if(c == '#'){
				obj = Instantiate(wall, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
				obj.name = "WALL";
				obj.tag = "Stage";
				obj.transform.parent = stage.transform;
				pos.x += 10.0f;
			}else if(c == 'T'){
				//obj = Instantiate(tree, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
				//obj.name = "Tree";
				//obj.tag = "Stage";
				//pos.x += 10.0f;
			}else if(c == '\n'){
				pos.z -= 10.0f;
				pos.x = originPos.x;
			}else if(c == ' '){
				pos.x += 10.0f;
			}
		}
	}

}