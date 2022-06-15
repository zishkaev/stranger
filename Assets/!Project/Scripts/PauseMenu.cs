using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public Text text;
	public Text resumeText;
	public GameObject resumeButton;

	public AudioSource audioSource;
	public AudioClip click;

	public GameObject saveButton;
	public Transform backButton;

	private bool isEndLevel;

	public static PauseMenu instance;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		GameController.instance.onPause += ShowPause;
		GameController.instance.onResume += Hide;
		GameController.instance.onLoadMenu += Hide;
		GameController.instance.onGameOver += ShowWinLoseGame;
		Hide();
	}

	public void ShowPause() {
		if (GameController.instance.IsGame()) {
			text.text = "pause";
			gameObject.SetActive(true);
		}
	}

	public void Hide() {
		resumeText.text = "resume";
		resumeButton.SetActive(true);
		saveButton.SetActive(true);
		backButton.localPosition = new Vector3(backButton.localPosition.x, -150, backButton.localPosition.z);
		gameObject.SetActive(false);
	}

	public void ShowWinLoseGame(bool state) {
		gameObject.SetActive(true);
 		text.text = state ? "win" : "lose";
		if (state) {
			resumeText.text = "next level";
			isEndLevel = true;
			if (GameController.instance.IsLastLevel()) {
				//GameController.instance.SetPause();
				gameObject.SetActive(false);
				GameController.instance.WinLastLevel();
			}
		} else {
			resumeButton.SetActive(false);
			saveButton.SetActive(false);
			backButton.localPosition = new Vector3(backButton.localPosition.x, -100, backButton.localPosition.z);
		}
	}

	public void ResumeGame() {
		PlayClickSound();
		if (!isEndLevel) {
			GameController.instance.SetPause();
		} else {
			GameController.instance.LoadNextLevel();
			isEndLevel = false;
		}
	}

	public void RestartGame() {
		PlayClickSound();
		GameController.instance.LoadGame();
	}

	public void SaveGame() {
		GameController.instance.SaveGame();
	}

	public void Menu() {
		PlayClickSound();
		GameController.instance.LoadMenu();
		isEndLevel = false;
	}

	private void PlayClickSound() {
		audioSource.PlayOneShot(click);
	}

	public void PlayHoverSound() { }

	private void OnDestroy() {
		GameController.instance.onPause -= ShowPause;
		GameController.instance.onResume -= Hide;
		GameController.instance.onLoadMenu -= Hide;
		GameController.instance.onGameOver -= ShowWinLoseGame;
	}
}
