using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObserver : MonoBehaviour {

	private void Awake() {
		GameController.instance.onSave += SaveGame;
		GameController.instance.onGameOver += GameOver;
	}
	private bool isGameOver = false;

	public void GameOver(bool state) {
		isGameOver = true;
	}

	public void SaveGame() {
		List<Enemy> enemies = Spawner.instance.spawnedEnemies;
		GameState gameState = new GameState();
		if (isGameOver) {
			gameState.curLevel = SceneController.instance.Level + 1;
			gameState.loadNextLevel = true;
		} else {
			gameState.curLevel = SceneController.instance.Level;
			gameState.spetial = Player.instance.Spetial;
			gameState.playerPosition = Player.instance.transform.position;
			gameState.playerRotation = Player.instance.transform.eulerAngles;
			gameState.health = (int)Player.instance.health;
			gameState.armor = (int)Player.instance.ammunition.armor;
			gameState.curWeapon = Player.instance.ammunition.CurWeapon;
			gameState.weaponProj = new int[3];
			for (int i = 0; i < Player.instance.ammunition.weapons.Length; i++) {
				gameState.weaponProj[i] = Player.instance.ammunition.weapons[i].GetProjectile();
			}

			gameState.wave = Spawner.instance.Wave;
			gameState.enemiesKilled = Spawner.instance.KillEnemies;
			gameState.leftSpawnEnemies = Spawner.instance.LeftEnemiesToSpawn;
			for (int i = enemies.Count - 1; i >= 0; --i) {
				if (enemies[i] == null || enemies[i].health <= 0) {
					enemies.RemoveAt(i);
				}
			}

			gameState.enemySaveStates = new EnemySaveState[enemies.Count];
			for (int i = 0; i < enemies.Count; i++) {
				EnemySaveState enemySave = new EnemySaveState();
				enemySave.enemyType = enemies[i].type;
				enemySave.enemyPosition = enemies[i].transform.position;
				enemySave.health = enemies[i].health;
				gameState.enemySaveStates[i] = enemySave;
			}
		}
		SaveController.instance.SaveGame(gameState);
	}

	private void OnDestroy() {
		GameController.instance.onSave -= SaveGame;
		GameController.instance.onGameOver -= GameOver;
	}
}
