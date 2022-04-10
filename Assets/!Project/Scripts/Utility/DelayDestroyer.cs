using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroyer : MonoBehaviour {
	public float delayTimeDestroy = 3f;

	private void Start() {
		Invoke(nameof(DestroyObject), delayTimeDestroy);
	}

	private void DestroyObject() {
		Destroy(gameObject);
	}
}
