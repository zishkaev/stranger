using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public Wave[] waves;
	public EnemyRandom[] enemyPrefabs;
	public Transform[] spawnPoints;

	public ValueUI valueUI;

	public float incMove = 3.5f;
	public float incRot = 60f;

	private int enemies;
	private int curWave = 0;

	private void Start() {
		StartWave();
	}

	public void StartWave() {
		enemies = waves[curWave].count;
		valueUI.SetValue(waves[curWave].count - enemies, waves[curWave].count);
		StartCoroutine(SpawnEnemies());
	}

	IEnumerator SpawnEnemies() {
		for (int i = 0; i < waves[curWave].startCount; i++) {
			int randP = UnityEngine.Random.Range(0, spawnPoints.Length);
			GameObject bot = Instantiate(GetEnemy(), spawnPoints[randP].position, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			enemy.onDead += DeadEnemy;
		}
		yield return new WaitForSeconds(waves[curWave].delaySpawn);
		int bots = waves[curWave].count - waves[curWave].startCount;
		for (int i = 0; i < bots; i++) {
			int randP = UnityEngine.Random.Range(0, spawnPoints.Length);
			GameObject bot = Instantiate(GetEnemy(), spawnPoints[randP].position, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			enemy.onDead += DeadEnemy;
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
		enemies -= 1;
		valueUI.SetValue(waves[curWave].count - enemies, waves[curWave].count);
		if (enemies <= 0) {
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
	public float rnd;
}
