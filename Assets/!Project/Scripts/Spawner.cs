using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public Wave[] waves;
	public GameObject enemyPrefab;
	public Transform[] spawnPoints;

	public Bonus[] bonus;
	public Transform[] bonusPoints;
	public float delayBonusTime;
	private float tBonus;
	private List<Bonus> bonusSpawn;

	public ValueUI valueUI;

	private int enemies;
	private int curWave = 0;

	private void Start() {
		StartWave();
	}

	private void Update() {
		if (tBonus < 0) {
			tBonus = delayBonusTime;
			SpawnBonus();
		}
		else {
			tBonus -= Time.deltaTime;
		}
	}
	public void SpawnBonus() {
		int randP = UnityEngine.Random.Range(0, bonusPoints.Length);
		Collider[] colliders = Physics.OverlapSphere(bonusPoints[randP].position, 1);
		for (int i = 0; i < colliders.Length; i++) {
			Bonus bonus = colliders[i].GetComponent<Bonus>();
			if (bonus) {
				bonus.DestroyBonus();
			}
		}
		int randB = UnityEngine.Random.Range(0, bonus.Length);
		Instantiate(bonus[randB], bonusPoints[randP].position, Quaternion.identity);
	}

	public void StartWave() {
		enemies = waves[curWave].count;
		valueUI.SetValue(waves[curWave].count - enemies, waves[curWave].count);
		StartCoroutine(SpawnEnemies());
	}

	IEnumerator SpawnEnemies() {
		for (int i = 0; i < waves[curWave].startCount; i++) {
			int randP = UnityEngine.Random.Range(0, spawnPoints.Length);
			GameObject bot = Instantiate(enemyPrefab, spawnPoints[randP].position, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			enemy.onDead += DeadEnemy;
		}
		yield return new WaitForSeconds(waves[curWave].delaySpawn);
		int bots = waves[curWave].count - waves[curWave].startCount;
		for (int i = 0; i < bots; i++) {
			int randP = UnityEngine.Random.Range(0, spawnPoints.Length);
			GameObject bot = Instantiate(enemyPrefab, spawnPoints[randP].position, Quaternion.identity);
			Enemy enemy = bot.GetComponent<Enemy>();
			enemy.onDead += DeadEnemy;
			yield return new WaitForSeconds(waves[curWave].delaySpawn);
		}
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
