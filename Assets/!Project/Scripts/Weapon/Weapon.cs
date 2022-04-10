using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public Type weaponType;
	public GameObject projectile;
	public Transform muzzle;
	public Action onShoot;

	protected bool isShooted;

	private int countProjectiles;
	private bool canShoot = true;

	private void OnEnable() {
		InputSystem.instance.onShoot += Shoot;
	}

	public void AddProjectiles(int count) {
		countProjectiles += count;
	}

	public void SetProjectiles(int count) {
		countProjectiles = count;
	}

	public int GetProjectile() {
		return countProjectiles;
	}

	public virtual void Shoot(bool state) {
		if (!canShoot) return;
		if (state) {
			if (countProjectiles > 0) {
				Instantiate(projectile, muzzle.position, muzzle.rotation);
				countProjectiles--;
				onShoot?.Invoke();
			}
		}
	}

	private void OnDisable() {
		InputSystem.instance.onShoot -= Shoot;
	}
}
