using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : Enemy {

	public float timeRelaxAfterAttack = 3f;

	public override void Attack() {
		base.Attack();
		Relax();
		relaxing = timeRelaxAfterAttack;
	}
}
