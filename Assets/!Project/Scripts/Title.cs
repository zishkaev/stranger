using UnityEngine;

public class Title : MonoBehaviour {
	public float t;

	private void Start() {
		Time.timeScale = 1f;
		Invoke(nameof(LoadMenu), t);
	}

	public void LoadMenu() {
		SceneController.instance.LoadMenu();
	}
}
