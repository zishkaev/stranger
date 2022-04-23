using System;
using UnityEngine;


public class Enemy : MonoBehaviour {
	public Animator animator;
	public EnemyMove enemyMove;
	public float health = 100f;
	public float damage = 10f;
	public float attackDist = 2f;
	public float attackTime = 1f;

	protected Player player;
	protected Transform playerTran;
	protected float t;
	protected bool isDead;

	public Action onDead;
	public bool relaxed = false;
	public float timeRelax = 2f;
	protected float relaxing;
	protected bool attackProc;

	protected virtual void Start() {
		player = Player.instance;
		playerTran = Player.instance.transform;
		t = attackTime;
	}

	protected virtual void Update() {
		if (isDead) return;
		if (relaxed) {
			relaxing -= Time.deltaTime;
			if (relaxing < 0) {
				relaxed = false;
			}
			return;
		}
		if (Vector3.Distance(transform.position, playerTran.position) < attackDist || attackProc) {
			Move(false);
			animator.SetBool("Attack", true);
			attackProc = true;
			if (t < 0) {
				Attack();
				t = attackTime;
				attackProc = false;
			}
			else {
				AttackProc();
				t -= Time.deltaTime;
			}
		}
		else {
			Move(true);
		}
	}

	public virtual void Move(bool state) {
		enemyMove.SetMove(state);
		animator.SetBool("Walk", state);
		if (state) {
			animator.SetBool("Attack", false);
		}
	}

	protected virtual void AttackProc() {

	}

	public virtual void Attack() {
		if (Vector3.Distance(transform.position, playerTran.position) < attackDist)
			player.Damage(damage);
	}

	public virtual void Damage(float damage) {
		if (isDead) return;
		health -= damage;
		if (!attackProc) {
			animator.SetTrigger("Hit");
			Relax();
		}
		if (health <= 0) {
			Dead();
		}
	}

	public void Relax() {
		relaxed = true;
		relaxing = timeRelax;
		Move(false);
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
