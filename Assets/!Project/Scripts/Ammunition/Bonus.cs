using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
	public Type type;
	public int count;

	public Action onGetBonus;

	private void OnTriggerEnter(Collider other) {
		Player player = other.GetComponentInParent<Player>();
		if (player) {
			onGetBonus?.Invoke();
			player.ammunition.AddBonus(this);
			DestroyBonus();
		}
	}

	public void DestroyBonus() {
		Destroy(gameObject);
	}
}
