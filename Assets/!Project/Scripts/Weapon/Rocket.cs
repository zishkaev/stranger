using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet {

	public float radiusExplosion = 10f;
	public GameObject explosion;

	private List<Enemy> enemies;

	protected override void Start() {
		base.Start();
		enemies = new List<Enemy>();
	}

	public override void Damage(Collider[] colliders) {
		Collider[] checkColliders = Physics.OverlapSphere(transform.position, radiusExplosion);
		bool find = false;
		for (int i = 0; i < checkColliders.Length; i++) {
			Enemy enemy = checkColliders[i].GetComponentInParent<Enemy>();
			if (enemy) {
				for (int j = 0; j < enemies.Count; j++) {
					if (enemy == enemies[j]) {
						find = true;
						break;
					}
				}
				if (!find) {
					float dist = Vector3.Distance(transform.position, checkColliders[i].transform.position);
					if (dist < radiusExplosion) {
						float multy = 1 - dist / radiusExplosion;
						float dam = damage * multy;
						enemy.Damage(dam);
					}
					enemies.Add(enemy);
				}
			}
			find = false;
		}
		DestroyBullet();
	}

	protected override void DestroyBullet() {
		Instantiate(explosion, transform.position, Quaternion.identity);
		base.DestroyBullet();
	}
}
