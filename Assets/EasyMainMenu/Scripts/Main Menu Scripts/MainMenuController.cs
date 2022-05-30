using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	Animator anim;
	public AudioSource audioSource;
	public AudioClip click;

	[Header("LoadSave")]
	public GameObject saveButton;
	public Transform backButton;

	[Header("Options Panel")]
	public GameObject MainOptionsPanel;
	public GameObject StartGameOptionsPanel;
	public GameObject GamePanel;
	public GameObject ControlsPanel;
	public GameObject GfxPanel;
	public GameObject LoadGamePanel;

	[Header("Settings")]
	public Slider sliderX;
	public Slider sliderY;
	public Toggle toggleMusic;
	public Slider sliderMusic;
	public Text aimSize;
	public Image aimImage;

	[Header("GraphicSettings")]
	public Text fullScreenText;
	public Text resolutionText;
	public Text shadowText;
	public Text qualityText;
	public Text aFText;
	public Text aAText;
	public Text vSync;

	// Use this for initialization
	private void Start() {
		//settings
		sliderX.value = Setting.instance.x;
		sliderY.value = Setting.instance.y;
		toggleMusic.isOn = Setting.instance.isMusic;
		sliderMusic.value = Setting.instance.source.volume;
		aimSize.text = Setting.instance.aimSize.ToString();
		aimImage.sprite = Setting.instance.AimImage;
		//graphics
		fullScreenText.text = Setting.instance.isFullScreen ? "on" : "off";
		Vector2 res = Setting.instance.Resolution;
		resolutionText.text = res.x + "x" + res.y;
		shadowText.text = Setting.instance.isShadow ? "on" : "off";
		qualityText.text = Setting.instance.Quality;
		aFText.text = Setting.instance.isAF ? "on" : "off";
		aAText.text = Setting.instance.curAntiAlias == 0 ? "no aa" : Setting.instance.curAntiAlias.ToString() + "x aa";
		vSync.text = Setting.instance.isVSync? "on" : "off";

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
		if (SaveController.instance.HasSaveGame) {
			saveButton.SetActive(true);
			backButton.localPosition = new Vector3(-68.60004f, -100, -449.9999f);
		} else {
			saveButton.SetActive(false);
			backButton.localPosition = new Vector3(-68.60004f, -50, -449.9999f);
		}

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

	//загрузка сохранения
	public void LoadSaveGame() {
		GameController.instance.LoadSaveGame();
	}
	#endregion

	#region Settings
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

	public void SetAimSize() {
		Setting.instance.NextAimSize();
		aimSize.text = Setting.instance.aimSize.ToString();
	}

	public void SetAimImage() {
		Setting.instance.NextAimImage();
		aimImage.sprite = Setting.instance.AimImage;
	}
	#endregion

	#region GraphicSettings
	public void SetResolution() {
		Setting.instance.NextResolution();
		Vector2 res = Setting.instance.Resolution;
		resolutionText.text = res.x + "x" + res.y;
	}

	public void SetFullScreen() {
		bool state = !Setting.instance.isFullScreen;
		Setting.instance.SetFullScreen(state);
		fullScreenText.text = Setting.instance.isFullScreen ? "on" : "off";
	}

	public void SetShadow() {
		bool state = !Setting.instance.isShadow;
		Setting.instance.SetShadow(state);
		shadowText.text = state ? "on" : "off";
	}

	public void SetQualityLevel() {
		Setting.instance.NextQualityLevel();
		qualityText.text = Setting.instance.Quality;
	}

	public void SetAnisotropicFiltering() {
		bool state = !Setting.instance.isAF;
		Setting.instance.SetAnisotropicFiltering(state);
		aFText.text = Setting.instance.isAF ? "on" : "off";
	}

	public void SetAntiAlias() {
		Setting.instance.NextAntiAlias();
		aAText.text = Setting.instance.curAntiAlias == 0 ? "no aa" : Setting.instance.curAntiAlias.ToString() + "x aa";
	}

	public void SetVSync() {
		bool state = !Setting.instance.isVSync;
		Setting.instance.SetVSync(state);
		vSync.text = Setting.instance.isVSync ? "on" : "off";
	}
	#endregion

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
		SaveController.instance.SaveSetting();
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
