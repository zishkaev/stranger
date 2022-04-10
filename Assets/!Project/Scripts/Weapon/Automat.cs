using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automat : Weapon {
	public float rateOfFire;
	private float timeShoot;

	private void Update() {
		if (isShooted) {
			Shoot();
		}
	}

	public void Shoot() {
		if (timeShoot <= 0) {
			base.Shoot(true);
			timeShoot = 1 / rateOfFire;
		}
		else {
			timeShoot -= Time.deltaTime;
		}
	}

	public override void Shoot(bool state) {
		isShooted = state;
	}
}
