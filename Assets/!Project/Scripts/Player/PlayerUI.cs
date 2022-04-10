using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	public Image blood;
	public float speedBlood;
	public ProgressBar healthBar;
	public ValueUI armorUI;
	public WeaponUI weaponUI;
	private Ammunition ammunition;
	private Player player;
	private Color startColorBlood;

	private void Start() {
		player = Player.instance;
		player.onDamage += DamageUI;
		ammunition = player.ammunition;
		ammunition.onChange += UpdateUI;
		player.onDamage += UpdateUI;
		for (int i = 0; i < ammunition.weapons.Length; i++) {
			ammunition.weapons[i].onShoot += UpdateUI;
		}
	}

	public void DamageUI() {
		StartCoroutine(Damage());
	}

	IEnumerator Damage() {
		blood.color = new Color(1, 0, 0, 1);
		yield return null;
		yield return null;
		float t = 1;
		while (t > 0) {
			t -= Time.deltaTime * speedBlood;
			blood.color = new Color(1, 0, 0, t);
			yield return null;
		}
	}

	public void UpdateUI() {
		healthBar.SetValue(player.health, player.maxHealth);
		armorUI.SetValue((int)ammunition.armor, (int)ammunition.maxArmor);
		Weapon weapon = ammunition.GetWeapon();
		weaponUI.SetWeapon(weapon.weaponType);
		weaponUI.SetValue(weapon.GetProjectile());
	}

	private void OnDestroy() {
		player.onDamage -= DamageUI;
		ammunition.onChange -= UpdateUI;
		player.onDamage -= UpdateUI;
		for (int i = 0; i < ammunition.weapons.Length; i++) {
			ammunition.weapons[i].onShoot -= UpdateUI;
		}
	}
}
