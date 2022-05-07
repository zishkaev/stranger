using UnityEngine;

public class Bullet : MonoBehaviour {
	public Type weaponType;
	public float speed;
	public float radiusBullet;
	public float damage;
	public bool isEnemy;

	public LayerMask layerMask;

	protected virtual void Start() { }

	protected virtual void FixedUpdate() {
		Vector3 checkPoint = transform.position + transform.forward * speed * Time.fixedDeltaTime;
		Collider[] colliders = Physics.OverlapSphere(checkPoint, radiusBullet, layerMask);
		if (colliders.Length > 0) {
			Damage(colliders);
		}
		transform.position = checkPoint;
	}

	public virtual void Damage(Collider[] colliders) {
		if (!isEnemy) {
			DamageEnemy(colliders[0]);
		} else {
			DamagePlayer(colliders[0]);
		}
		DestroyBullet();
	}

	public void DamageEnemy(Collider collision) {
		Enemy enemy = collision.GetComponentInParent<Enemy>();
		if (enemy) {
			enemy.Damage(damage);
		}
	}

	public void DamagePlayer(Collider collision) {
		Player player = collision.GetComponentInParent<Player>();
		if (player) {
			player.Damage(damage);
		}
	}

	protected virtual void DestroyBullet() {
		Destroy(gameObject);
	}
}
