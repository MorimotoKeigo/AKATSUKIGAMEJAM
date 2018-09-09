using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;


	//public
	public int PlayerHP;
	public int LightCount;

	public int EnemyAttckDamege;

	private void Awake()
	{
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}

		InitGame();

		DontDestroyOnLoad(gameObject);
	}

	void Reset(){

		InitGame ();

		StageCreater scripts = GameObject.Find ("StageCreater").GetComponent<StageCreater> ();
		scripts.ResetStage ();
	
	
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (PlayerHP < 0) {
			Invoke ("Reset", 2f);
		} 
	}

	void InitGame(){
		PlayerHP = 100;
		LightCount = 3;
	}
		
	void EnemyAttack(){
		PlayerHP -= 10;
	}

	int LightNum(){
		return LightCount;	
	}

	void AddLight(){

		if (LightCount < 3) {
			LightCount++;
		}
	}

	void SubLight(){
		if (LightCount > 0) {
			LightCount--;
		}
	}
}
