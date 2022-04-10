using UnityEngine;

public class PlayerLook : MonoBehaviour {
	public Transform camera;

	public float 
		sensitivityX = 1.0f,
		sensitivityY = 1.0f,
		minX = -90.0f, 
		maxX = 90.0f;

	private Quaternion targetRot;
	private Quaternion cameraRot;

	private bool isActive = true;

	private void Start() {
		cameraRot = camera.rotation;
		targetRot = transform.rotation;
	}

	private void Update() {
		if (!isActive) return;

		float rotY = InputSystem.instance.GetMouse().x * sensitivityX;
		float rotX = InputSystem.instance.GetMouse().y * sensitivityY;

		targetRot *= Quaternion.Euler(0.0f, rotY, 0.0f);
		cameraRot *= Quaternion.Euler(-rotX, 0.0f, 0.0f);

		transform.localRotation = targetRot;
		camera.localRotation = cameraRot;
	}

	public void SetActive(bool state) {
		isActive = state;
	}
}
