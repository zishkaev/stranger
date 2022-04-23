using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour {
	public static Setting instance;

	public float x = 1f;
	public float y = 1f;
	public bool isMusic;

	public AudioSource source;

	void Start() {
		instance = this;
	}

	public void Music(bool state) {
		isMusic = state;
		if (state) {
			source.Play();
		} else {
			source.Stop();
		}
	}

	public void ChangeVolume(float volume) {
		source.volume = volume;
	}
}
