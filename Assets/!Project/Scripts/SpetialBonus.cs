using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpetialBonus : MonoBehaviour {

	public float speedRnd = 0.5f;
	public float jumpRnd = 1f;

	public bool isSpeed;
	public bool isJump;

	private void Start() {
		float rnd = UnityEngine.Random.Range(0f, 1f);
		if (rnd < speedRnd) {
			isSpeed = true;
		}
		else if (rnd < jumpRnd) {
			isJump = true;
		}
	}

	private void OnTriggerEnter(Collider other) {
		Player player = other.GetComponentInParent<Player>();

		if(player != null) {
			if (isSpeed) {
				player.AddSpetialBonus(SpetialBonusEnum.speed);
			}
			else if(isJump) {
				player.AddSpetialBonus(SpetialBonusEnum.jump);
			}
			Destroy(gameObject);
		}
	}
}
