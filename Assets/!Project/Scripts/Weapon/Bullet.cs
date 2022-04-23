using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
	public Type weaponType;
	public float force;
	public float damage;
	public Rigidbody rigidbody;
	public bool isEnemy;

	protected virtual void Start() {
		rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		if (!isEnemy) {
			DamageEnemy(collision);
		} else {
			DamagePlayer(collision);
		}
	}

	public void DamageEnemy(Collision collision) {
		Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
		if (enemy) {
			enemy.Damage(damage);
			DestroyBullet();
		}
	}

	public void DamagePlayer(Collision collision) {
		Player player = collision.collider.GetComponentInParent<Player>();
		if (player) {
			player.Damage(damage);
			DestroyBullet();
		}
	}

	protected virtual void DestroyBullet() {
		Destroy(gameObject);
	}
}
