using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon {

	public float reloadTime = 2f;
	private float t = 2f;

	public float ProgressReload => t / reloadTime;

	private void Start() {
		t = reloadTime;
	}

	public override void Shoot(bool state) {
		if (isShooted) return;
		if (state) {
			base.Shoot(state);
			isShooted = true;
			t = 0f;
		}
	}

	private void Update() {
		if (isShooted) {
			t += Time.deltaTime;
			if (t >= reloadTime) {
				t = reloadTime;
				isShooted = false;
			}
		}
	}
}
