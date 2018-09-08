using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

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

	}
}
