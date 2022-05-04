using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {
	public Enemy enemy;
	public ProgressBar progressBar;

	private float maxHealth;

	private void Start() {
		maxHealth = enemy.health;
		enemy.onDead += OnDead;
	}

	private void Update() {
		progressBar.SetValue(enemy.health, maxHealth);
	}

	private void OnDead() {
		enemy.onDead -= OnDead;
		gameObject.SetActive(false);
	}
}
