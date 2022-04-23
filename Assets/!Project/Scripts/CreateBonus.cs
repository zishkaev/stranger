using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBonus : MonoBehaviour {
	public BonusRandom[] bonus;
	public float delayBonusTime;
	private float tBonus;
	private Bonus spawnedBonus;
	public GameObject beam;

	private void Update() {
		if (tBonus < 0) {
			tBonus = delayBonusTime;
			SpawnBonus();
		} else {
			tBonus -= Time.deltaTime;
		}
		if (spawnedBonus != null) {
			
		}
	}

	public void SpawnBonus() {
		if(spawnedBonus != null) {
			spawnedBonus.DestroyBonus();
		}
		//Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
		Bonus bonus;
		//for (int i = 0; i < colliders.Length; i++) {
		//	bonus = colliders[i].GetComponent<Bonus>();
		//	if (bonus) {
		//		bonus.DestroyBonus();
		//	}
		//}
		bonus = GetBonus();
		if (bonus != null) {
			spawnedBonus = Instantiate(bonus, transform.position, Quaternion.identity);
			spawnedBonus.onGetBonus += HideBeam;
			beam.SetActive(true);
		} else {
			SpawnBonus();
		}
	}

	public void HideBeam() {
		beam.SetActive(false);
		if (spawnedBonus)
			spawnedBonus.onGetBonus -= HideBeam;
	}

	public Bonus GetBonus() {
		float randB = UnityEngine.Random.Range(0.0f, 1.0f);
		for(int i = 0; i < bonus.Length; i++) {
			if (bonus[i].rnd > randB)
				return bonus[i].bonus;
		}
		return null;
	}
}

[Serializable]
public struct BonusRandom {
	public Bonus bonus;
	public float rnd;
}
