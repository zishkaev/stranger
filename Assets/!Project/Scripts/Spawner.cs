using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public Wave[] waves;
	public EnemyRandom[] enemyPrefabs;
	public Transform[] spawnPoints;
	public GameObject spetialBonus;
	public ValueUI valueUI;

	public float incMove = 3.5f;
	public float incRot = 60f;

	private int curLiveEnemies;
	private int curWave = 0;
	private int leftEnemies;
	public List<Enemy> spawnedEnemies;

	public static Spawner instance;

	public Action<Enemy> OnSpawn;

	public int Wave => curWave;

	public int LeftEnemiesToSpawn => leftEnemies;

	public int KillEnemies => waves[curWave].count - curLiveEnemies;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		curLiveEnemies = 0;
		if (!GameController.instance.isLoadGame)
			StartWave();
		else
			LoadWave();
	}

	public void LoadWave() {
		GameState gameState = SaveController.instance.CurSaveState;

		Player.instance.transform.position = gameState.playerPosition;
		Player.instance.transform.rotation = Quaternion.Euler(gameState.playerRotation);
		Player.instance.health = gameState.health;
		Player.instance.ammunition.armor = gameState.armor;
		Player.instance.ammunition.SetWeapon(gameState.curWeapon);
		Player.instance.AddSpetialBonus(gameState.spetial);
		if(gameState.spetial != SpetialBonusEnum.none) {
			Destroy(spetialBonus);
		}
		for (int i = 0; i < Player.instance.ammunition.weapons.Length; i++) {
			Player.instance.ammunition.weapons[i].SetProjectiles(gameState.weaponProj[i]);
		}
		curWave = gameState.wave;
		curLiveEnemies = waves[curWave].count - gameState.enemiesKilled;
		valueUI.SetValue(waves[curWave].count - curLiveEnemies, waves[curWave].count);
		for (int i = 0; i < gameState.enemySaveStates.Length; i++) {
			EnemySaveState enemySaveState = gameState.enemySaveStates[i];
			GameObject bot = Instantiate(GetEnemy(enemySaveState.enemyType), enemySaveState.enemyPosition, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			enemy.health = enemySaveState.health;
			spawnedEnemies.Add(enemy);
			enemy.onDead += DeadEnemy;
		}
		StartCoroutine(SpawnEnemiesAfterLoad(gameState.leftSpawnEnemies));
		SaveController.instance.DeleteSaveGame();
		GameController.instance.LoadedSaveGame();
	}

	public void StartWave() {
		curLiveEnemies = waves[curWave].count;
		spawnedEnemies = new List<Enemy>();
		valueUI.SetValue(waves[curWave].count - curLiveEnemies, waves[curWave].count);
		StartCoroutine(SpawnEnemies());
	}

	IEnumerator SpawnEnemies() {
		for (int i = 0; i < waves[curWave].startCount; i++) {
			int randP = UnityEngine.Random.Range(0, spawnPoints.Length);
			GameObject bot = Instantiate(GetEnemy(), spawnPoints[randP].position, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			spawnedEnemies.Add(enemy);
			OnSpawn?.Invoke(enemy);
			enemy.onDead += DeadEnemy;
		}
		int bots = leftEnemies = waves[curWave].count - waves[curWave].startCount;
		yield return new WaitForSeconds(waves[curWave].delaySpawn);
		for (int i = 0; i < bots; i++) {
			int randP = UnityEngine.Random.Range(0, spawnPoints.Length);
			GameObject bot = Instantiate(GetEnemy(), spawnPoints[randP].position, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			spawnedEnemies.Add(enemy);
			OnSpawn?.Invoke(enemy);
			enemy.onDead += DeadEnemy;
			leftEnemies = bots - 1 - i;
			yield return new WaitForSeconds(waves[curWave].delaySpawn);
		}
	}

	IEnumerator SpawnEnemiesAfterLoad(int leftEnemiesToSpawn) {
		int bots = leftEnemiesToSpawn;
		for (int i = 0; i < bots; i++) {
			int randP = UnityEngine.Random.Range(0, spawnPoints.Length);
			GameObject bot = Instantiate(GetEnemy(), spawnPoints[randP].position, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			spawnedEnemies.Add(enemy);
			OnSpawn?.Invoke(enemy);
			enemy.onDead += DeadEnemy;
			leftEnemies = bots - i;
			yield return new WaitForSeconds(waves[curWave].delaySpawn);
		}
	}

	public GameObject GetEnemy() {
		float randB = UnityEngine.Random.Range(0.0f, 1.0f);
		for (int i = 0; i < enemyPrefabs.Length; i++) {
			if (enemyPrefabs[i].rnd > randB)
				return enemyPrefabs[i].enemy;
		}
		return null;
	}

	public GameObject GetEnemy(EnemyType type) {
		for (int i = 0; i < enemyPrefabs.Length; i++) {
			if (enemyPrefabs[i].type == type) {
				return enemyPrefabs[i].enemy;
			}
		}
		return null;
	}

	public void EndWave() {
		StopAllCoroutines();
		curWave++;
		if (curWave >= waves.Length) {
			GameController.instance.DelayGameOver();
			return;
		}
		Invoke(nameof(StartWave), waves[curWave - 1].delayNextWave);
	}

	public void DeadEnemy() {
		curLiveEnemies -= 1;
		valueUI.SetValue(waves[curWave].count - curLiveEnemies, waves[curWave].count);
		if (curLiveEnemies <= 0) {
			EndWave();
		}
	}
}

[Serializable]
public struct Wave {
	public int count;
	public int startCount;
	public float delaySpawn;
	public float delayNextWave;
}

[Serializable]
public struct EnemyRandom {
	public GameObject enemy;
	public EnemyType type;
	public float rnd;
}
