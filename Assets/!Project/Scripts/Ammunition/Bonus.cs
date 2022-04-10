using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
	public Type type;
	public int count;

	private void OnTriggerEnter(Collider other) {
		Player player = other.GetComponentInParent<Player>();
		if (player) {
			player.ammunition.AddBonus(this);
			DestroyBonus();
		}
	}

	public void DestroyBonus() {
		Destroy(gameObject);
	}
}
