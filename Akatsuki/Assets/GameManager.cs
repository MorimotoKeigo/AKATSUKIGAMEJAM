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

	public int RTime = 180;
	//残り時間
	public static int remainTime;
	//現在の時間
	private float currentTime;
	//Cast
	private int cTime;
	private int oldTime;


	private void Awake()
	{
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}

		InitGame();

		//DontDestroyOnLoad(gameObject);
	}

	void Reset(){

		InitGame ();

		StageCreater scripts = GameObject.Find ("StageCreater").GetComponent<StageCreater> ();
		scripts.ResetStage ();
	
	
	}

	// Use this for initialization
	void Start () {
		//Time系
		currentTime = 0;
		remainTime = RTime;
		cTime = 0;
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


		if (remainTime == 0) {
			Invoke ("GameOver",2f);
		}

		Debug.Log ("Tresure_num"+tresure_num);

		if (tresure_num == 0) {
			GameClear ();
		}
			
		timeCount ();
		Debug.Log (remainTime);
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
		SceneManager.LoadScene("GameClear");
	}

	void GetTresure(){
		tresure_num--;
	}


	void timeCount()
	{

		//現在の時間
		currentTime += Time.deltaTime;
		//int型
		cTime = (int)currentTime;


		oldTime = cTime;

		//残り時間
		if (remainTime != 0) {
			remainTime = RTime - cTime;
		}

		/*
		if (timeText != null)
		{
			var displayTime = Mathf.Clamp(remainTime, 0, RTime - 5);
			timeText.text = displayTime.ToString();
			if(remainTime < 10) { timeText.color = Color.red; }

			if (remainTime == 0)
			{
				if (timeText.text != "Finish!!")
				{
					SoundManager.instance.PlayRandomSound(finishSound);
					cutInImage.SetActive(true);
				}
				timeText.text = "<size=60>Finish!!</size>";
			}
		}
		*/
	}

	public static int getClearTime(){
		return remainTime;
	}
}
