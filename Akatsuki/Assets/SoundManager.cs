using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	[Range(0f,1f)]
	public float bgmVolume = 1f;
	[Range(0f, 1f)]
	public float seVolume = 1f;

	public AudioSource efSource; //SoundEffect
	public AudioSource bgmSource; //Bgm

	//singleton
	public static SoundManager instance = null;

	// Use this for initialization
	void Awake () {

		if(SoundManager.instance == null){
			instance = this;
		}else if(SoundManager.instance != this){
			Destroy(gameObject);
		}

		//DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		bgmSource.volume = bgmVolume;
		efSource.volume = seVolume;
	}

	public void PlaySingleSound(AudioClip clip)
	{
		bgmSource.clip = clip;
		bgmSource.Play();
	}

	public void PlayRandomSound(params AudioClip[] clips)
	{
		int ramdomIndex = Random.Range(0,clips.Length);

		efSource.clip = clips[ramdomIndex];
		efSource.Play();
	}


}
