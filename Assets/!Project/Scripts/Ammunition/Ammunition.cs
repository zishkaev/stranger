using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour {
	public float armor;
	public float maxArmor;
	[SerializeField] public Weapon[] weapons;
	private int curWeapon;
	public AudioSource source;
	public AudioClip takeBonus;
	public Action onChange;

	public int CurWeapon => curWeapon;

	private void Start() {
		curWeapon = 0;
		InputSystem.instance.onChangeWeapon += ChangeWeapon;
		for (int i = 0; i < weapons.Length; i++) {
			weapons[i].SetProjectiles(10);
		}
		ChangeWeapon();
	}

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

	public void ChangeWeapon() {
		weapons[curWeapon].gameObject.SetActive(false);
		curWeapon++;
		if (curWeapon >= weapons.Length)
			curWeapon = 0;
		weapons[curWeapon].gameObject.SetActive(true);
		onChange?.Invoke();
	}

	public void SetWeapon(int n) {
		if (n >= weapons.Length || n < 0) return;
		weapons[curWeapon].gameObject.SetActive(false);
		curWeapon = n;
		weapons[curWeapon].gameObject.SetActive(true);
		onChange?.Invoke();
	}

	public void AddHealth(int count) {
		Player.instance.AddHealth(count);
		onChange?.Invoke();
	}

	public void AddArmor(int count) {
		armor += count;
		if (armor > maxArmor)
			armor = maxArmor;
		onChange?.Invoke();
		Debug.Log("Add armor " + count + ". Armor = " + armor);
	}

	public void AddBonus(Bonus bonus) {
		source.PlayOneShot(takeBonus);
		if (bonus.type == Type.armor) {
			AddArmor(bonus.count);
			onChange?.Invoke();
			return;
		}
		if (bonus.type == Type.health) {
			AddHealth(bonus.count);
			onChange?.Invoke();
			return;
		}

		if (bonus.type == Type.projectile) {
			int bCount = bonus.count;
			int randProj = 0;
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

	public Weapon GetWeapon() {
		return weapons[curWeapon];
	}

	private void OnDestroy() {
		InputSystem.instance.onChangeWeapon -= ChangeWeapon;
	}
}

[Serializable]
public enum Type {
	pistol,
	automat,
	rocket,

	armor,
	health,
	projectile
}
