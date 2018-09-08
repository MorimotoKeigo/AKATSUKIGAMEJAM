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
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

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
