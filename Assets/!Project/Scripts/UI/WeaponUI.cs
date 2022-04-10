using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class WeaponUI : ValueUI {
	public TypeImage[] typeImages;

	public void SetWeapon(Type type) {
		for(int i = 0; i< typeImages.Length; i++) {
			if (typeImages[i].type == type) {
				typeImages[i].image.gameObject.SetActive(true);
			}
			else {
				typeImages[i].image.gameObject.SetActive(false);
			}
		}
	}
}
[Serializable]
public struct TypeImage{
	public Type type;
	public Image image;
}