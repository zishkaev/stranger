using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	Animator anim;
	public AudioSource audioSource;
	public AudioClip click;

	[Header("Options Panel")]
	public GameObject MainOptionsPanel;
	public GameObject StartGameOptionsPanel;
	public GameObject GamePanel;
	public GameObject ControlsPanel;
	public GameObject GfxPanel;
	public GameObject LoadGamePanel;

	public Slider sliderX;
	public Slider sliderY;
	public Toggle toggleMusic;
	public Slider sliderMusic;


	// Use this for initialization
	private void Start() {
		sliderX.value = Setting.instance.x;
		sliderY.value = Setting.instance.y;
		toggleMusic.isOn = Setting.instance.isMusic;
		sliderMusic.value = Setting.instance.source.volume;


		anim = GetComponent<Animator>();
	}

	#region Open Different panels

	//открыть настройки
	public void openOptions() {
		//enable respective panel
		MainOptionsPanel.SetActive(true);
		StartGameOptionsPanel.SetActive(false);

		//play anim for opening main options panel
		anim.Play("buttonTweenAnims_on");

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
		anim.Play("buttonTweenAnims_on");

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
		anim.Play("OptTweenAnim_on");

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
		anim.Play("OptTweenAnim_on");

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
		anim.Play("OptTweenAnim_on");

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
		anim.Play("OptTweenAnim_on");

		//play click sfx
		playClickSound();

	}

	//начало игры
	public void newGame() {
		GameController.instance.ResetLevel();
		GameController.instance.LoadGame();
	}
	#endregion

	public void SetX(float x) {
		Setting.instance.x = x;
	}

	public void SetY(float y) {
		Setting.instance.y = y;
	}

	public void SetMusic(bool check) {
		Setting.instance.Music(check);
	}

	public void SetVolume(float volume) {
		Setting.instance.ChangeVolume(volume);
	}

	#region Back Buttons

	//выход из опций
	public void back_options() {
		anim.Play("buttonTweenAnims_off");
		playClickSound();
	}

	//выход из настроек
	public void back_options_panels() {
		anim.Play("OptTweenAnim_off");
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
	public void playHoverClip() {}

	void playClickSound() {
		audioSource.PlayOneShot(click);
	}


	#endregion
}
