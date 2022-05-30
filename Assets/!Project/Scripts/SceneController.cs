using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string menuScene;
    public string[] gameScenes;
	public string titleScene;

    public static SceneController instance;

	private int curGameScene;

	public int GameSceneCount => gameScenes.Length;

	public int Level => curGameScene;

	public bool IsGameScene => SceneManager.GetActiveScene().name != menuScene && SceneManager.GetActiveScene().name != titleScene;

	private void Awake() {
        instance = this;
	}

	public void LoadMenu() {
		ScreenFade.instance.onEnd += LoadMenuScene;
		ScreenFade.instance.Show();
	}

	public void LoadTitle() {
		ScreenFade.instance.onEnd += LoadTitleScene;
		ScreenFade.instance.Show();
	}

	public void LoadTitleScene() {
		ScreenFade.instance.onEnd -= LoadTitleScene;
		LoadScene(titleScene);
	}

	public void LoadSceneEnd() {
		ScreenFade.instance.Hide();
	}

	public void LoadScene(string scene) {
		StartCoroutine(LoadYourAsyncScene(scene));
		//SceneManager.LoadScene(scene);
	}

	public void LoadMenuScene() {
		ScreenFade.instance.onEnd -= LoadMenuScene;
		LoadScene(menuScene);
	}

	public void LoadGameScene() {
		ScreenFade.instance.onEnd -= LoadGameScene;
		LoadScene(gameScenes[curGameScene]);
	}

	public void LoadLevel(int n) {
		curGameScene = n;
		ScreenFade.instance.onEnd += LoadGameScene;
		ScreenFade.instance.Show();
	}

	IEnumerator LoadYourAsyncScene(string scene) {
		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
		// a sceneBuildIndex of 1 as shown in Build Settings.

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone) {
			yield return null;
		}
		LoadSceneEnd();
	}
}
