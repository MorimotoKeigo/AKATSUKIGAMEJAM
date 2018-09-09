using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	//public
	public int PlayerHP;
	public int LightCount;

	public int EnemyAttckDamege;

	public GameObject[] tresure;
	public int tresure_num = 5;

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

		//tresure = GameObject.Find ("Tresure");
		//tresure_num = tresure.GetLength ();
		//tresure_num = 2;
		Debug.Log ("Tresure_num"+tresure_num);
	}

	// Update is called once per frame
	void Update () {
		
		if (PlayerHP < 0) {
			Invoke ("GameOver", 2f);
		} 
		Debug.Log ("Tresure_num"+tresure_num);

		if (tresure_num == 0) {
			GameClear ();
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

	void GameOver(){
		SceneManager.LoadScene("GameOver");
	}


	void GameClear(){
		Debug.Log ("GameClear");
	}

	void GetTresure(){
		tresure_num--;
	}
}
