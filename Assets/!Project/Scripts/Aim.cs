using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour {
	public Image aimImage;

	private void Start() {
		aimImage.sprite = Setting.instance.AimImage;
		float size = 0f;
		switch (Setting.instance.aimSize) {
			case AimSize.Small:
				size = 0.25f;
				break;
			case AimSize.Medium:
				size = 0.5f;
				break;
			case AimSize.Big:
				size = 0.75f;
				break;
		}
		aimImage.rectTransform.localScale = Vector3.one * size;
	}
}
