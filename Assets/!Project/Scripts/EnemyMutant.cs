using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMutant : Enemy {
	public GameObject shockEffect;
	public Transform hip;

	public override void Attack() {
		attackProc = false;
		Vector3 pos = hip.position;
		pos.y = transform.position.y;
		transform.position = pos;
		
		pos = transform.position;
		pos.y = hip.position.y;
		hip.position = pos;
		Instantiate(shockEffect, transform.position, Quaternion.identity);
		t = attackTime;
	}
}
