using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    public Image bar;
	public RocketLauncher weapon;

	private void Start() {
		weapon.onActivate += SetActive;
		if (!weapon.gameObject.activeInHierarchy) {
			SetActive(false);
		}
	}

	public void SetActive(bool state) {
		gameObject.SetActive(state);
	}

	private void Update() {
		bar.fillAmount = 1 - weapon.ProgressReload;
	}

	private void OnDestroy() {
		if (weapon)
			weapon.onActivate -= SetActive;
	}
}
