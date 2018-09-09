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

	private int num = 0; // 何マス目か判別するint

	void Start () {

		// Already StageData
		GameObject[] stages = GameObject.FindGameObjectsWithTag("UI_Stage");
		foreach(GameObject stage in stages) {
			GameObject.DestroyImmediate(stage);
		}

		UI_CreateStage();
	}


	void UI_CreateStage(){
		string stageTextData = textAsset.text;

		foreach(char c in stageTextData){

			GameObject obj = null;



			if(c == 'F' || c == 'T'){
				obj = Instantiate(floor_UI, Map_parent.transform) as GameObject;
				obj.name = "UI_floor" + num;
				obj.tag = "UI_Stage";
				num++;
			} else if (c == '#') { // else(c == '#')
				obj = Instantiate(wall_UI, Map_parent.transform) as GameObject;
				obj.name = "UI_wall" + num;
				obj.tag = "UI_Stage";
				num++;
			}

		}
	}

}
