using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {
	public static InputSystem instance;

	public Action<bool> onShoot;
	public Action onJump;
	public Action onChangeWeapon;
	public Action onSit;
	public Action onPause;

	private Vector3 move;
	private XY cameraRot;

	private void Awake() {
		instance = this;
	}

	private void Update() {
		//move
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		move = new Vector3(x, 0, y);
		//mouse
		float rotX = Input.GetAxis("Mouse X");
		float rotY = Input.GetAxis("Mouse Y");
		cameraRot.x = rotX;
		cameraRot.y = rotY;
		//shoot
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			onShoot?.Invoke(true);
		}
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			onShoot?.Invoke(false);
		}
		//sit
		if (Input.GetKeyDown(KeyCode.C)) {
			onSit?.Invoke();
		}
		//jump
		if (Input.GetKeyDown(KeyCode.Space)) {
			onJump?.Invoke();
		}
		//change weapon
		if (Input.GetKeyDown(KeyCode.Q)) {
			onChangeWeapon?.Invoke();
		}
		//pause
		if (Input.GetKeyDown(KeyCode.Escape)) {
			onPause?.Invoke();
		}
	}

	public Vector3 GetMove() {
		return move;
	}

	public XY GetMouse() {
		return cameraRot;
	}
}

[Serializable]
public struct XY {
	public float x;
	public float y;
}
