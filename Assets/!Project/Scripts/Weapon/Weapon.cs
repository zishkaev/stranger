using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public Type weaponType;
	public GameObject projectile;
	public Transform muzzle;
	public Action onShoot;
	public Action<bool> onActivate;

	public AudioSource source;
	public AudioClip shootSound;

	protected bool isShooted;

	private int countProjectiles;
	private bool canShoot = true;

	protected virtual void OnEnable() {
		InputSystem.instance.onShoot += Shoot;
		onActivate?.Invoke(true);
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
				source.PlayOneShot(shootSound);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				RaycastHit hit;
				if (!Physics.Raycast(ray, out hit, 1000)) {
					muzzle.localEulerAngles = Vector3.zero;
				} else {
					muzzle.forward = hit.point - muzzle.position;
				}
				Instantiate(projectile, muzzle.position, muzzle.rotation);
				countProjectiles--;
				onShoot?.Invoke();
			}
		}
	}

	private void OnDisable() {
		InputSystem.instance.onShoot -= Shoot;
		onActivate?.Invoke(false);
	}
}
