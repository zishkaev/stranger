using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public static Player instance;
	public PlayerMover playerMover;
	public PlayerLook playerLook;
	public PlayerJump playerJump;
	public Ammunition ammunition;
	public float health;
	public float maxHealth;
	public Action onDamage;

	private void Awake() {
		instance = this;
		health = maxHealth;
		GameController.instance.onLoadGame += Init;
		GameController.instance.onPause += Pause;
		GameController.instance.onResume += Resume;
		GameController.instance.onGameOver += StopGame;
	}

	public void Init() {
		health = 100f;
	}

	public void AddHealth(int count) {
		health += count;
		if (health > 100)
			health = 100;
		Debug.Log("Add health " + count + ". Health = " + health);
	}

	public void Damage(float damage) {
		if (IsDead()) return;
		health -= ammunition.DecreaseDamage(damage);
		Debug.Log("Damaged " + damage + " Health " + health);
		if (IsDead()) {
			Dead();
		}
		onDamage?.Invoke();
	}

	public bool IsDead() {
		return health <= 0;
	}

	public void Pause() {
		playerMover.SetActive(false);
		playerLook.SetActive(false);
		playerJump.SetActive(false);
	}

	public void Resume() {
		playerMover.SetActive(true);
		playerLook.SetActive(true);
		playerJump.SetActive(true);
	}

	public void StopGame(bool state) {
		Pause();
	}

	public void Dead() {
		playerMover.SetActive(false);
		playerLook.SetActive(false);
		health = 0;
		Debug.Log("You Dead!");
		GameController.instance.GameOver();
	}

	private void OnDestroy() {
		GameController.instance.onLoadGame -= Init;
		GameController.instance.onPause -= Pause;
		GameController.instance.onResume -= Resume;
	}
}
