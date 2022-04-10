using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour {
	public Rigidbody rigidbody;
	public float speed;

	[Header("Jump")]
	public float jumpForce;
	public string tagGround;
	public Transform basePoint;
	private bool jump;
	private bool checkLanding;
	private bool isActive = true;

	private void Start() {
		InputSystem.instance.onJump += Jump;
	}

	// Update is called once per frame
	private void Update() {
		if (!isActive) return;
		
		if (jump)
			CheckLanding();
	}

	private void FixedUpdate() {
		Move();
	}

	public void Move() {
		//transform.position = transform.position + transform.forward * InputSystem.instance.GetMove().z * speed + transform.right * InputSystem.instance.GetMove().x * speed;
		Vector3 f = transform.forward * InputSystem.instance.GetMove().z * speed;
		Vector3 r = transform.right * InputSystem.instance.GetMove().x * speed;
		Vector3 u = new Vector3(0, rigidbody.velocity.y, 0);
		rigidbody.velocity = f + r + u;
	}

	public void SetActive(bool state) {
		isActive = state;
	}

	public void Jump() {
		if (jump) return;
		jump = true;
		rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}

	public void CheckLanding() {
		Collider[] colliders = Physics.OverlapSphere(basePoint.position, 0.1f);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].CompareTag(tagGround)) {
				if (checkLanding == false)
					return;
				jump = false;
				return;
			}
		}
		checkLanding = true;
	}

	private void OnDestroy() {
		InputSystem.instance.onJump -= Jump;
	}
}
