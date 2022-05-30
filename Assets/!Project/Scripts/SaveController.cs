using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour {
	public static SaveController instance;

	public bool HasSaveGame => gameState != null;
	public int CurSaveLevel => gameState.curLevel;

	public GameState CurSaveState => gameState;

	public SaveSettingClass saveSetting;
	private GameState gameState;

	private string 
		pathSave, 
		pathSettings;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		pathSave = Application.streamingAssetsPath + "/" + "save.json";
		pathSettings = Application.streamingAssetsPath + "/" + "settings.json";
		LoadSettings();
		LoadSave();
	}

	public void LoadSave() {
		if (!File.Exists(pathSave)) {
			Debug.Log("Gamedata no exists");
			return;
		} else {
			using (StreamReader stream = new StreamReader(pathSave)) {
				string json = stream.ReadToEnd();
				gameState = JsonUtility.FromJson<GameState>(json);
			}
		}
	}

	public void LoadSettings() {
		if (!File.Exists(pathSettings)) {
			Debug.Log("SettingsData no exists, create new settingsfile");
			SaveSetting();
			return;
		} else {
			using (StreamReader stream = new StreamReader(pathSettings)) {
				string json = stream.ReadToEnd();
				saveSetting = JsonUtility.FromJson<SaveSettingClass>(json);
				Setting.instance.SetSettings(saveSetting);
			}
		}
	}

	public void SaveGame(GameState game) {
		gameState = game;
		using (StreamWriter stream = new StreamWriter(pathSave)) {
			string json = JsonUtility.ToJson(gameState);
			stream.Write(json);
		}
	}

	public void SaveSetting() {
		using (StreamWriter stream = new StreamWriter(pathSettings)) {
			saveSetting = Setting.instance.GetSettings();
			string json = JsonUtility.ToJson(saveSetting);
			stream.Write(json);
			Debug.Log("SettingsData save");
		}
	}

	public void DeleteSaveGame() {
		if (File.Exists(pathSave)) {
			File.Delete(pathSave);
			gameState = new GameState();
		}
	}
}

[Serializable]
public class GameState {
	public bool loadNextLevel;
	public int curLevel;

	public Vector3 playerPosition;
	public Vector3 playerRotation;
	public SpetialBonusEnum spetial;
	public int curWeapon;
	public int health;
	public int armor;
	public int[] weaponProj;

	public int wave;
	public int enemiesKilled;
	public int leftSpawnEnemies;
	public EnemySaveState[] enemySaveStates;
}

[Serializable]
public class EnemySaveState {
	public EnemyType enemyType;
	public Vector3 enemyPosition;
	public float health;
}
