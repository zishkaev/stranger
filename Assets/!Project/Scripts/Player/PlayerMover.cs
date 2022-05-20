using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	public Rigidbody rigidbody;
	public CapsuleCollider capsuleCollider;
	public float sitWalkSpeed;
	public float walkSpeed;
	public float runSpeed;
	private float curSpeed;
	private bool isActive = true;

	public PlayerJump playerJump;

	public GameObject ui;

	public AudioSource sourceStep;
	public AudioSource sourceRun;

	public LayerMask layerMask;

	private bool isSit;

	private void Start() {
		InputSystem.instance.onSit += SetSit;
	}

	private void FixedUpdate() {
		if (!isActive) return;
		Move();
		if (!isSit) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				curSpeed = runSpeed;
			} else {
				curSpeed = walkSpeed;
			}
		}
	}

	public void IncSpeed() {
		walkSpeed *= 1.2f;
		runSpeed *= 1.2f;
		ui.SetActive(true);
	}

	public void SetSit() {
		isSit = !isSit;
		curSpeed = sitWalkSpeed;
		Player.instance.playerLook.SetSit(isSit);
	}

	public void Move() {
		Vector3 f = transform.forward * InputSystem.instance.GetMove().z * curSpeed * Time.fixedDeltaTime;
		Vector3 r = transform.right * InputSystem.instance.GetMove().x * curSpeed * Time.fixedDeltaTime;
		if (Physics.CheckSphere(transform.position + Vector3.up * 2f - Vector3.up * capsuleCollider.radius * 0.75f + f + r, capsuleCollider.radius * 0.75f, layerMask, QueryTriggerInteraction.Ignore)) {
			f = Vector3.zero;
			r = Vector3.zero;
		}
		rigidbody.MovePosition(transform.position + f + r);
		if ((f + r).magnitude > float.Epsilon && !playerJump.jumpProc) {
			if (curSpeed == walkSpeed) {
				sourceStep.volume = 1f;
				sourceRun.volume = 0f;
			} else {
				sourceStep.volume = 0f;
				sourceRun.volume = 1f;
			}
		} else {
			sourceStep.volume = 0f;
			sourceRun.volume = 0f;
		}
	}

	public void SetActive(bool state) {
		isActive = state;
		sourceRun.enabled = state;
		sourceStep.enabled = state;
	}

	private void OnDestroy() {
		InputSystem.instance.onSit -= SetSit;
	}
}
