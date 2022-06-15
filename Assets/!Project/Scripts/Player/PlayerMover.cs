using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	public Rigidbody rigidbody;//физичный компонент
	public CapsuleCollider capsuleCollider;//колайдер
	public float sitWalkSpeed;//скорость в сидячем положении
	public float walkSpeed;//скорость ходьбы
	public float runSpeed;//скорость бега
	private float curSpeed;//текущая скорость
	private bool isActive = true;//активность

	public PlayerJump playerJump;//компонент прыжка

	public GameObject ui;//ui прокаченной скорость перемещения

	public AudioSource sourceStep;//звук ходьбы
	public AudioSource sourceRun;//звук бега

	public LayerMask layerMask;//слой для определения столкновения со стенами

	private bool isSit;//сидит ли игрок

	private void Start() {
		InputSystem.instance.onSit += SetSit;//подписка на событие приседа
	}

	private void FixedUpdate() {
		if (!isActive) return;//проверка активности
		Move();//перемещение
		if (!isSit) {//если не сидячее положение, то проверяем нажата ли кнопка shift
			if (Input.GetKey(KeyCode.LeftShift)) {//если нажата кнопка, то скорость перемещения становится больше
				curSpeed = runSpeed;
			} else {//иначе скорость становится обычной
				curSpeed = walkSpeed;
			}
		}
	}

	//увеличение скорости, если взят бонус на скорость
	public void IncSpeed() {
		walkSpeed *= 1.2f;
		runSpeed *= 1.2f;
		ui.SetActive(true);
	}
	//установка сидячего положения
	public void SetSit() {
		isSit = !isSit;
		curSpeed = sitWalkSpeed;
		Player.instance.playerLook.SetSit(isSit);
	}
	//перемещение
	public void Move() {
		//перемещение вперед и вбок
		Vector3 f = transform.forward * InputSystem.instance.GetMove().z * curSpeed * Time.fixedDeltaTime;
		Vector3 r = transform.right * InputSystem.instance.GetMove().x * curSpeed * Time.fixedDeltaTime;
		//проверка можно ли сдвинуться в этом направлении, если нет то f и r сбрасывается на 0
		if (Physics.CheckSphere(transform.position + Vector3.up * 2f - Vector3.up * capsuleCollider.radius * 0.75f + f + r, capsuleCollider.radius * 0.75f, layerMask, QueryTriggerInteraction.Ignore)) {
			f = Vector3.zero;
			r = Vector3.zero;
		}
		//перемещение в направлении
		rigidbody.MovePosition(transform.position + f + r);
		//если происходит перемещение и не прыжок то звук ходьбы или бега звучит
		if ((f + r).magnitude > float.Epsilon && !playerJump.jumpProc) {
			if (curSpeed == walkSpeed) {
				sourceStep.volume = 1f;
				sourceRun.volume = 0f;
			} else {
				sourceStep.volume = 0f;
				sourceRun.volume = 1f;
			}
		} else { //иначе звук бега и ходьбы становится 0
			sourceStep.volume = 0f;
			sourceRun.volume = 0f;
		}
	}
	//задается будет ли работать данный компонент или нет
	public void SetActive(bool state) {
		isActive = state;
		if (sourceRun)
			sourceRun.enabled = state;
		if (sourceStep)
			sourceStep.enabled = state;
	}
	//при уничтожении отписывается от события приседа
	private void OnDestroy() {
		InputSystem.instance.onSit -= SetSit;
	}
}
