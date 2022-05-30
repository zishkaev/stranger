using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public int currentLevel;

	public static GameController instance;

	public Action
		onLoadGame,
		onLoadMenu,
		onPause,
		onResume,
		onSave;
	public Action<bool> onGameOver;
	public bool isLoadGame;
	private bool pause;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		InputSystem.instance.onPause += SetPause;
		LoadMenu();
	}

	public void LoadNextLevel() {
		currentLevel++;
		LoadGame();
	}

	public void ResetLevel() {
		currentLevel = 0;
	}

	public bool IsLastLevel() {
		return currentLevel == SceneController.instance.GameSceneCount - 1;
	}

	public void WinLastLevel() {
		SceneController.instance.LoadTitle();
	}

	public void LoadSaveGame() {
		if (SaveController.instance.CurSaveState.loadNextLevel) {
			currentLevel = SaveController.instance.CurSaveLevel;
			LoadGame();
			SaveController.instance.DeleteSaveGame();
		} else {
			isLoadGame = true;
			currentLevel = SaveController.instance.CurSaveLevel;
			LoadGame();
		}
	}

	public void LoadedSaveGame() {
		isLoadGame = false;
	}

	public void SaveGame() {
		onSave?.Invoke();
	}

	public void LoadGame() {
		Pause(false);
		SceneController.instance.LoadLevel(currentLevel);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
		onLoadGame?.Invoke();
	}

	public void LoadMenu() {
		pause = false;
		Time.timeScale = 1;
		SceneController.instance.LoadMenu();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		onLoadMenu?.Invoke();
	}

	public void SetPause() {
		if (IsGame()) {
			if (pause)
				Pause(false);
			else
				Pause(true);
		}
	}

	public void Pause(bool state) {
		Cursor.visible = state;
		pause = state;
		Debug.Log("Pause " + state);
		if (state) {
			Time.timeScale = 0;
			onPause?.Invoke();
			Cursor.lockState = CursorLockMode.None;
		}
		else {
			Time.timeScale = 1;
			onResume?.Invoke();
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void GameOver() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;
		if (Player.instance.IsDead()) {
			Debug.Log("Game LOSE!!!");
			onGameOver?.Invoke(false);
		}
		else {
			Debug.Log("Game WIN!!!");
			onGameOver?.Invoke(true);
		}
	}

	public void DelayGameOver() {
		Invoke(nameof(GameOver), 5f);
	}

	public bool IsGame() {
		return SceneController.instance.IsGameScene;
	}

	public bool IsPause() {
		return pause;
	}
}
