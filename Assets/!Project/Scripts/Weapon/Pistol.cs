using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

	public override void Shoot(bool state) {
		if (state && isShooted) {
			return;
		}
		else if (isShooted) {
			isShooted = false;
			return;
		}
		base.Shoot(state);
		isShooted = true;
	}
}
