using System;
using UnityEngine;


public class EnemyShooter : Enemy {
	public GameObject bullet;
	public Transform muzzle;

	public override void Attack() {
		GameObject spawnBul = Instantiate(bullet, muzzle.position, Quaternion.identity);
		spawnBul.transform.forward = Player.instance.transform.position - muzzle.position;
	}

	protected override void AttackProc() {
		transform.forward = Player.instance.transform.position - transform.position;
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
