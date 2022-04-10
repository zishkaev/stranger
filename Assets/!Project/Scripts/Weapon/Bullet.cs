using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
	public Type weaponType;
	public float force;
	public float damage;
	public Rigidbody rigidbody;

	protected virtual void Start() {
		rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
		if (enemy) {
			enemy.Damage(damage);
			DestroyBullet();
		}
	}

	protected virtual void DestroyBullet() {
		Destroy(gameObject);
	}
}
