using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripts : MonoBehaviour {

	public enum Direction{
		LEFT = 0,
		RIGHT = 1,
		UP = 2,
		DOWN = 3
	}

	Direction CurrentDirection;

	public int enemyID;

	public int EnemyRange;

	private GameObject player;

	private float timeElapsed;

	public AudioClip enemySound;


	GameObject redRamp;
	GameObject blueRamp;
	GameObject whiteRamp;



	// Use this for initialization
	void Start () {

		/*
		if (enemyID == 0) {
			CurrentDirection = Direction.RIGHT;
		} else if (enemyID == 1) {
			CurrentDirection = Direction.UP;
		}
		*/

		player = GameObject.Find ("Player");

	}
	
	// Update is called once per frame
	void Update () {

		if (CheckRedLight() == false) {
			CheckPlayer ();
		}
		Attack ();
			
	}


	void CheckPlayer(){

		//player = GameObject.Find ("Player");

		//Debug.Log (player.transform.position);

		float dis = Vector3.Distance (player.transform.position, transform.position);

		float disW;
		float disH;
		float disX;
		float disZ;

		if (dis < 30f) {


			//disX = Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x);
			//disZ = player.transform.position.z - transform.position.z;

			//disW = Mathf.Abs (disX);
			//disH = Mathf.Abs (disZ);


			if (transform.position.x < player.transform.position.x) {
				transform.Translate (0.05f, 0f, 0f);
			} else{
				transform.Translate (-0.05f, 0f, 0f);
			}


			if (transform.position.z > player.transform.position.z) {
				transform.Translate (0.0f, 0f, -0.05f);
			} else{
				transform.Translate (0.0f, 0f, 0.05f);
			}



		}
	}

	void Attack(){

		timeElapsed += Time.deltaTime;

		if (timeElapsed >= 1f) {


			float dis = Vector3.Distance (player.transform.position, transform.position);

			if (dis < 10f) {
				//SoundManager.instance.PlaySingleSound(enemySound);
				GameManager.instance.PlayerHP -= 5;
			}

			timeElapsed = 0f;
		}

		
	}
		
	bool CheckRedLight(){


		redRamp = GameObject.Find ("Light(red)");

		if (redRamp != null) {

			float dis = Vector3.Distance (redRamp.transform.position, transform.position);

			if (dis < 15f) {
				return true;
			}
			return false;
		} else {
			return false;
		}
	}


	void OnCollisionEnter(Collision collision){
		//Debug.Log ("call");

		/*
		Direction nextDirection;

		nextDirection = Direction.LEFT;

		if (collision.gameObject.tag == "Stage") {

			if (CurrentDirection == Direction.RIGHT) {
				nextDirection = Direction.LEFT;
			} else if (CurrentDirection == Direction.LEFT) {
				nextDirection = Direction.RIGHT;
			} else if (CurrentDirection == Direction.UP) {
				nextDirection = Direction.DOWN;
			} else if (CurrentDirection == Direction.DOWN) {
				nextDirection = Direction.UP;
			}
		}
			
		CurrentDirection = nextDirection;
		*/
	}
}
