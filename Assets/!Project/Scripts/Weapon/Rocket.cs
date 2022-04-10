using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet {

	public float radius = 10f;
	public GameObject explosion;

	private List<Enemy> enemies;

	protected override void Start() {
		base.Start();
		enemies = new List<Enemy>();
	}

	protected override void OnCollisionEnter(Collision collision) {
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		bool find = false;
		for (int i = 0; i < colliders.Length; i++) {
			Enemy enemy = colliders[i].GetComponentInParent<Enemy>();
			if (enemy) {
				for (int j = 0; j < enemies.Count; j++) {
					if (enemy == enemies[j]) {
						find = true;
					}
				}
				if (!find) {
					float dist = Vector3.Distance(transform.position, colliders[i].transform.position);
					if (dist < radius) {
						float multy = 1 - dist / radius;
						float dam = damage * multy;
						enemy.Damage(dam);
					}
					enemies.Add(enemy);
				}
			}
		}
		DestroyBullet();
	}

	protected override void DestroyBullet() {
		Instantiate(explosion, transform.position, Quaternion.identity);
		base.DestroyBullet();
	}
}
