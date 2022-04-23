using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
	public float radius;
	public float damage;

	private void Start() {
		Explostion();
	}

	public void Explostion() {
		float dist = Vector3.Distance(Player.instance.transform.position, transform.position);
		if (dist > radius) return;
		float curDamage = (1 - dist / radius) * damage;
		Player.instance.Damage(curDamage);
	}
}
