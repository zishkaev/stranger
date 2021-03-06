using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
	public Transform target;

	private void Start() {
		if (target == null)
			target = Player.instance.playerLook.camera;
	}

	private void Update() {
		transform.LookAt(target);
	}
}
