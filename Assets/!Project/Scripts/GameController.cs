using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public int currentLevel; //текущий уровень

	public static GameController instance;

	//главные события
	public Action
		onLoadGame,
		onLoadMenu,
		onPause,
		onResume,
		onSave;
	public Action<bool> onGameOver;//конец игры, true если выиграл, false если проиграл
	public bool isLoadGame; //если игра загружена из сохранения
	private bool pause;//включена ли пауза или нет

	private void Awake() {
		instance = this;
	}

	private void Start() {
		//подписываемся на события нажатия на кнопку и пропуска уровня
		InputSystem.instance.onPause += SetPause;
		InputSystem.instance.onSkipLevel += SkipLevel;
		//загрузка меню
		LoadMenu();
	}

	//переход на следующий уровень
	public void LoadNextLevel() {
		currentLevel++; //след номер уровня
		if (currentLevel >= SceneController.instance.GameSceneCount) { // если уровень больше чем количество, то переходим на сцену тайтла
			SceneController.instance.LoadTitle();
			return;
		}
		LoadGame();//загрузка уровня
	}

	//сброс текущего уровня
	public void ResetLevel() {
		currentLevel = 0;
	}

	//проверка что это послений уровень
	public bool IsLastLevel() {
		return currentLevel == SceneController.instance.GameSceneCount - 1;
	}

	//переход на сцену тайтла
	public void WinLastLevel() {
		SceneController.instance.LoadTitle();
	}

	//загрузка сохраненый игры
	public void LoadSaveGame() {
		//если сохранение было в конце игры, запускается след уровень
		if (SaveController.instance.CurSaveState.loadNextLevel) {
			currentLevel = SaveController.instance.CurSaveLevel;
			LoadGame();
			SaveController.instance.DeleteSaveGame();
		} else {
			//иначе будет загружена сохраненная сцена, и указана что игра должна быть не с нуля, а сохраненого места
			isLoadGame = true;
			currentLevel = SaveController.instance.CurSaveLevel;
			LoadGame();
		}
	}

	//сохарененая сцена была загружена
	public void LoadedSaveGame() {
		isLoadGame = false;
	}

	//сохранение игры
	public void SaveGame() {
		onSave?.Invoke();
	}

	//загрузка игры
	public void LoadGame() {
		Pause(false); //сброс паузы
		SceneController.instance.LoadLevel(currentLevel);//загрузка уровня
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
		onLoadGame?.Invoke();
	}

	//загрузка меню
	public void LoadMenu() {
		//сброс паузы
		pause = false;
		Time.timeScale = 1;
		SceneController.instance.LoadMenu();//загрузка меню
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		onLoadMenu?.Invoke();
	}

	//ставится пауза
	public void SetPause() {
		if (IsGame()) {
			if (pause)
				Pause(false);
			else
				Pause(true);
		}
	}

	//пауза
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
	//пропуск уровня
	public void SkipLevel() {
		if (SceneController.instance.IsGameScene && pause) {
			PauseMenu.instance.gameObject.SetActive(false);
			LoadNextLevel();
		}
	}
	//конец игры
	public void GameOver() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;
		//проверка умер ли игрок
		if (Player.instance.IsDead()) {
			Debug.Log("Game LOSE!!!");
			onGameOver?.Invoke(false);
		}
		else {
			Debug.Log("Game WIN!!!");
			onGameOver?.Invoke(true);
		}
	}

	//загрузка конца игры через определенное время
	public void DelayGameOver() {
		Invoke(nameof(GameOver), 5f);
	}

	//проверка играем ли сейчас
	public bool IsGame() {
		return SceneController.instance.IsGameScene;
	}
	//проверка что сейчас пауза
	public bool IsPause() {
		return pause;
	}
	//при уничтожении объекта происходит отписка от событий подписанных ранее
	private void OnDestroy() {
		InputSystem.instance.onPause -= SetPause;
		InputSystem.instance.onSkipLevel -= SkipLevel;
	}
}
