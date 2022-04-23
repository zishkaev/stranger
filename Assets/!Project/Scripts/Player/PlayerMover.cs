using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	public Rigidbody rigidbody;
	public float walkSpeed;
	public float runSpeed;
	private float curSpeed;
	private bool isActive = true;

	private void FixedUpdate() {
		if (!isActive) return;
		Move();
		if (Input.GetKey(KeyCode.LeftShift)) {
			curSpeed = runSpeed;
		} else {
			curSpeed = walkSpeed;
		}
	}

	public void Move() {
		Vector3 f = transform.forward * InputSystem.instance.GetMove().z * curSpeed * Time.fixedDeltaTime;
		Vector3 r = transform.right * InputSystem.instance.GetMove().x * curSpeed * Time.fixedDeltaTime;
		rigidbody.transform.position += f + r;
	}

	public void SetActive(bool state) {
		isActive = state;
	}
}
