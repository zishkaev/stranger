using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour {
	public float armor;//текущая броня
	public float maxArmor;//макс броня
	[SerializeField] public Weapon[] weapons;//оружия
	private int curWeapon;//индекс текущего оружия
	public AudioSource source;//источник звук
	public AudioClip takeBonus;//звук взятия бонуса
	public Action onChange;//событие изменения данных

	public int CurWeapon => curWeapon;//индекс текущего оружия

	private void Start() {
		curWeapon = 0;//ставится первое оружия
		InputSystem.instance.onChangeWeapon += ChangeWeapon;//подписка на событие смены оружия
		for (int i = 0; i < weapons.Length; i++) {//задается начальное количество снарядов в оружии
			weapons[i].SetProjectiles(10);
		}
		ChangeWeapon();//смена оружия
	}

	//уменьшении урона с использованием брони при получении урона
	public float DecreaseDamage(float damage) {
		if (armor - damage > 0) {
			armor -= damage;
			Debug.Log("Lost armor " + damage + ". Armor = " + armor);
			return 0;
		}
		else {
			float dam = damage - armor;
			armor = 0;
			return dam;
		}
	}

	//смена оружия
	public void ChangeWeapon() {
		weapons[curWeapon].gameObject.SetActive(false);
		curWeapon++;
		if (curWeapon >= weapons.Length)
			curWeapon = 0;
		weapons[curWeapon].gameObject.SetActive(true);
		onChange?.Invoke();
	}
	//задается текущее оружие
	public void SetWeapon(int n) {
		if (n >= weapons.Length || n < 0) return;
		weapons[curWeapon].gameObject.SetActive(false);
		curWeapon = n;
		weapons[curWeapon].gameObject.SetActive(true);
		onChange?.Invoke();
	}
	//добавление жизни при взятии бонуса
	public void AddHealth(int count) {
		Player.instance.AddHealth(count);
		onChange?.Invoke();
	}
	//добавление брони при взятии бонуса
	public void AddArmor(int count) {
		armor += count;
		if (armor > maxArmor)
			armor = maxArmor;
		onChange?.Invoke();
		Debug.Log("Add armor " + count + ". Armor = " + armor);
	}
	//добавление бонуса
	public void AddBonus(Bonus bonus) {
		source.PlayOneShot(takeBonus);//звук бонуса
		if (bonus.type == Type.armor) {//если бонус это броня
			AddArmor(bonus.count);
			onChange?.Invoke();
			return;
		}
		if (bonus.type == Type.health) {//если бонус это жизни
			AddHealth(bonus.count);
			onChange?.Invoke();
			return;
		}

		if (bonus.type == Type.projectile) {//если бонус это снаряды
			int bCount = bonus.count;
			int randProj = 0;
			//распределение количество патроно от бонуса между оружием случайным образом
			for (int i = 0; i < weapons.Length; i++) {
				randProj = UnityEngine.Random.Range(1, bCount);
				weapons[i].AddProjectiles(randProj);
				bCount -= randProj;
				onChange?.Invoke();
				Debug.Log("Weapon " + weapons[i].weaponType + ". Add bullet " + bonus.count + ". bullet = " + weapons[i].GetProjectile());
				if (bCount == 0)
					break;
			}
			return;
		}

		//for (int i = 0; i < weapons.Length; i++) {
		//	if (weapons[i].weaponType == bonus.type) {
		//		weapons[i].AddProjectiles(bonus.count);
		//		onChange?.Invoke();
		//		Debug.Log("Weapon " + weapons[i].weaponType + ". Add bullet " + bonus.count + ". bullet = " + weapons[i].GetProjectile());
		//	}
		//}
	}
	//получаем текущее оружие
	public Weapon GetWeapon() {
		return weapons[curWeapon];
	}
	//при уничтожении отписывается от события смены оружия
	private void OnDestroy() {
		InputSystem.instance.onChangeWeapon -= ChangeWeapon;
	}
}

[Serializable]
public enum Type {//типы оружия и бонусов
	pistol,
	automat,
	rocket,

	armor,
	health,
	projectile
}
