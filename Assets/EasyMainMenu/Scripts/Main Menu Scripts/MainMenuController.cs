using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	Animator anim;

	public string newGameSceneName;
	public int quickSaveSlotID;

	[Header("Options Panel")]
	public GameObject MainScreenPanel;
	public GameObject MainOptionsPanel;
	public GameObject StartGameOptionsPanel;
	public GameObject GamePanel;
	public GameObject ControlsPanel;
	public GameObject GfxPanel;
	public GameObject LoadGamePanel;
	public GameObject PauseGameOptions;

	// Use this for initialization
	private void Start() {
		anim = GetComponent<Animator>();

		//new key
		PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);

		GameController.instance.onPause += openPause_Menu;
		GameController.instance.onResume += hideAllMenu;
		GameController.instance.onLoadMenu += openStartGameOptions;
	}

	#region Open Different panels

	//открыть настройки
	public void openOptions() {
		//enable respective panel
		MainOptionsPanel.SetActive(true);
		StartGameOptionsPanel.SetActive(false);

		//play anim for opening main options panel
		//anim.Play("buttonTweenAnims_on");

		//play click sfx
		playClickSound();

		//enable BLUR
		//Camera.main.GetComponent<Animator>().Play("BlurOn");

	}
	//открыть начало игры
	public void openStartGameOptions() {
		//enable respective panel
		MainOptionsPanel.SetActive(false);
		StartGameOptionsPanel.SetActive(true);

		//play anim for opening main options panel
		//anim.Play("buttonTweenAnims_on");

		//play click sfx
		playClickSound();

		//enable BLUR
		//Camera.main.GetComponent<Animator>().Play("BlurOn");

	}
	//открыть настройки игры
	public void openOptions_Game() {
		//enable respective panel
		GamePanel.SetActive(true);
		ControlsPanel.SetActive(false);
		GfxPanel.SetActive(false);
		LoadGamePanel.SetActive(false);

		//play anim for opening game options panel
		//anim.Play("OptTweenAnim_on");

		//play click sfx
		playClickSound();

	}
	//настройки управления
	public void openOptions_Controls() {
		//enable respective panel
		GamePanel.SetActive(false);
		ControlsPanel.SetActive(true);
		GfxPanel.SetActive(false);
		LoadGamePanel.SetActive(false);

		//play anim for opening game options panel
		//anim.Play("OptTweenAnim_on");

		//play click sfx
		playClickSound();

	}
	//настройки графики
	public void openOptions_Gfx() {
		//enable respective panel
		GamePanel.SetActive(false);
		ControlsPanel.SetActive(false);
		GfxPanel.SetActive(true);
		LoadGamePanel.SetActive(false);

		//play anim for opening game options panel
		//anim.Play("OptTweenAnim_on");

		//play click sfx
		playClickSound();

	}
	//сохранения
	public void openContinue_Load() {
		//enable respective panel
		GamePanel.SetActive(false);
		ControlsPanel.SetActive(false);
		GfxPanel.SetActive(false);
		LoadGamePanel.SetActive(true);

		//play anim for opening game options panel
		//anim.Play("OptTweenAnim_on");

		//play click sfx
		playClickSound();

	}
	//меню паузы
	public void openPause_Menu() {
		PauseGameOptions.SetActive(true);

		playClickSound();
	}
	//скрыть все меню
	public void hideAllMenu() {
		MainScreenPanel.SetActive(false);
		MainOptionsPanel.SetActive(false);
		StartGameOptionsPanel.SetActive(false);
		GamePanel.SetActive(false);
		ControlsPanel.SetActive(false);
		GfxPanel.SetActive(false);
		LoadGamePanel.SetActive(false);
		PauseGameOptions.SetActive(false);
	}
	//начало игры
	public void newGame() {
		if (!string.IsNullOrEmpty(newGameSceneName))
			GameController.instance.LoadGame();
		//SceneManager.LoadScene(newGameSceneName);
		else
			Debug.Log("Please write a scene name in the 'newGameSceneName' field of the Main Menu Script and don't forget to " +
				"add that scene in the Build Settings!");
	}
	#endregion

	#region Back Buttons

	//выход из опций
	public void back_options() {
		//anim.Play("buttonTweenAnims_off");
		playClickSound();
	}

	//выход из настроек
	public void back_options_panels() {
		//anim.Play("OptTweenAnim_off");
		playClickSound();

	}

	//выход
	public void Quit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
	#endregion

	#region Sounds
	public void playHoverClip() {

	}

	void playClickSound() {

	}


	#endregion
}
