using System;
using UnityEngine;


public class Enemy : MonoBehaviour {
	public Animator animator;
	public EnemyMove enemyMove;
	public float health = 100f;
	public float damage = 10f;
	public float attackDist = 2f;
	public float attackTime = 1f;

	private Player player;
	private Transform playerTran;
	private float t;
	private bool isDead;

	public Action onDead;

	private void Start() {
		player = Player.instance;
		playerTran = Player.instance.transform;
		t = attackTime;
	}

	private void Update() {
		if (isDead) return;
		if (Vector3.Distance(transform.position, playerTran.position) < attackDist) {
			Move(false);
			animator.SetBool("Attack", true);
			if (t < 0) {
				Attack();
			}
			else {
				t -= Time.deltaTime;
			}
		}
		else {
			Move(true);
		}
	}

	public void Move(bool state) {
		enemyMove.SetMove(state);
		if (state) {
			animator.SetBool("Walk", state);
		}
		animator.SetBool("Attack", false);
	}

	public virtual void Attack() {
		player.Damage(damage);
		t = attackTime;
	}

	public virtual void Damage(float damage) {
		if (isDead) return;
		health -= damage;
		animator.SetTrigger("Hit");
		if (health <= 0) {
			Dead();
		}
	}

	public virtual void Dead() {
		animator.SetTrigger("Dead");
		isDead = true;
		Move(false);
		onDead?.Invoke();
		Invoke(nameof(DestroyEnemy), 3f);
	}

	public void DestroyEnemy() {
		Destroy(gameObject);
	}
}
