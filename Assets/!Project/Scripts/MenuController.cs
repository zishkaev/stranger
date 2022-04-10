using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	public Button
		start,
		exit,
		resume,
		restart,
		menu;

	public GameObject textWin;
	public GameObject textLose;
	public GameObject menuRoot;
	public GameObject pauseRoot;

	private void Start() {
		GameController.instance.onLoadGame += Hide;
		GameController.instance.onLoadMenu += ShowMenu;
		GameController.instance.onPause += ShowPause;
		GameController.instance.onResume += Hide;
		GameController.instance.onGameOver += ShowGameOver;

		start.onClick.AddListener(StartGame);
		exit.onClick.AddListener(Exit);
		resume.onClick.AddListener(Resume);
		restart.onClick.AddListener(StartGame);
		menu.onClick.AddListener(Menu);

		ShowMenu();
	}

	public void ShowMenu() {
		menuRoot.SetActive(true);
	}

	public void ShowPause() {
		pauseRoot.SetActive(true);
	}

	public void ShowGameOver(bool state) {
		pauseRoot.SetActive(true);
		resume.gameObject.SetActive(false);
		if (state) {
			textWin.SetActive(true);
		}
		else {
			textLose.SetActive(true);
		}
	}

	public void Hide() {
		menuRoot.SetActive(false);
		pauseRoot.SetActive(false);
		resume.gameObject.SetActive(true);
		textWin.SetActive(false);
		textLose.SetActive(false);
	}

	public void StartGame() {
		GameController.instance.LoadGame();
	}

	public void Resume() {
		GameController.instance.Pause(false);
	}

	public void Menu() {
		Hide();
		GameController.instance.LoadMenu();
	}

	public void Exit() {
		Application.Quit();
	}

	private void OnDestroy() {
		GameController.instance.onLoadGame -= Hide;
		GameController.instance.onLoadMenu -= ShowMenu;
		GameController.instance.onPause -= ShowPause;
		GameController.instance.onResume -= Hide;

		start.onClick.RemoveListener(StartGame);
		exit.onClick.RemoveListener(Exit);
		resume.onClick.RemoveListener(Resume);
		restart.onClick.RemoveListener(StartGame);
		menu.onClick.RemoveListener(Menu);
	}
}
