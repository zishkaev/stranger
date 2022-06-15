using System;
using UnityEngine;

public class Setting : MonoBehaviour {
	public static Setting instance;

	public float maxX;
	public float maxY;

	public float x = 1f;
	public float y = 1f;
	public bool isMusic;
	private float volume = 1f;

	[Header("Audio")]
	public AudioSource source;

	[Header("AimSetting")]

	public int curAimSize = 0;
	public AimSize aimSize = AimSize.Small;
	public int curAimImage = 0;
	public Sprite[] aimImages;
	public Sprite AimImage => aimImages[curAimImage];

	[Header("Graphic")]
	public bool isShadow = true;
	public int curQuality = 0;
	public string[] qualityLevels;
	public string Quality => qualityLevels[curQuality];

	public int curResolution = 0;
	public Vector2[] resolutions;
	public Vector2 Resolution => resolutions[curResolution];

	public bool isFullScreen = true;
	public FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;

	public bool isAF = true;

	public int curAntiAlias = 2;

	public bool isVSync = true;

	private void Awake() {
		instance = this;
	}

	public void SetSettings(SaveSettingClass save) {
		x = save.x;
		y = save.y;
		Music(save.isMusic);
		ChangeVolume(save.volume);
		SetResolution(save.curResolution);
		SetFullScreen(save.isFullScreen);
		SetShadow(save.isShadow);
		SetQualityLevel(save.curQuality);
		SetAnisotropicFiltering(save.isAF);
		SetAntiAlias(save.curAntiAlias);
		SetVSync(save.isVSync);
		SetAimSize(save.curAimSize);
		SetAimImage(save.curAimImage);
	}

	public SaveSettingClass GetSettings() {
		SaveSettingClass saveSetting = new SaveSettingClass();
		saveSetting.x = x;
		saveSetting.y = y;
		saveSetting.isMusic = isMusic;
		saveSetting.volume = volume;
		saveSetting.curAimSize = curAimSize;
		saveSetting.curAimImage = curAimImage;
		saveSetting.isShadow = isShadow;
		saveSetting.curQuality = curQuality;
		saveSetting.curResolution = curResolution;
		saveSetting.isFullScreen = isFullScreen;
		saveSetting.isAF = isAF;
		saveSetting.curAntiAlias = curAntiAlias;
		saveSetting.isVSync = isVSync;
		return saveSetting;
	}

	public void Music(bool state) {
		isMusic = state;
		if (state) {
			source.Play();
		} else {
			source.Stop();
		}
	}

	public void ChangeVolume(float volume) {
		this.volume = volume;
		source.volume = volume;
	}

	public void NextResolution() {
		int res = ++curResolution;
		SetResolution(res);
	}

	public void SetResolution(int res) {
		curResolution = res;
		if (curResolution >= resolutions.Length) {
			curResolution = 0;
		}
		Screen.SetResolution((int)resolutions[curResolution].x, (int)resolutions[curResolution].y, fullScreenMode);
	}

	public void SetFullScreen(bool state) {
		isFullScreen = state;
		fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
		Screen.fullScreen = isFullScreen;
	}

	public void SetShadow(bool state) {
		isShadow = state;
		QualitySettings.shadows = state ? ShadowQuality.All : ShadowQuality.Disable;
	}

	public void NextQualityLevel() {
		int qua = ++curQuality;
		SetQualityLevel(qua);
	}

	public void SetQualityLevel(int qua) {
		curQuality = qua;
		if (curQuality >= qualityLevels.Length)
			curQuality = 0;
		QualitySettings.SetQualityLevel(curQuality, true);
	}

	public void SetAnisotropicFiltering(bool state) {
		isAF = state;
		QualitySettings.anisotropicFiltering = isAF ? AnisotropicFiltering.ForceEnable : AnisotropicFiltering.Disable;
	}

	public void NextAntiAlias() {
		int aa = curAntiAlias * 2;
		if (aa == 0)
			aa = 2;
		if (aa == 16)
			aa = 0;
		SetAntiAlias(aa);
	}

	public void SetAntiAlias(int aa) {
		curAntiAlias = aa;
		QualitySettings.antiAliasing = curAntiAlias;
	}

	public void SetVSync(bool state) {
		isVSync = state;
		QualitySettings.vSyncCount = isVSync ? 1 : 0;
	}

	public void NextAimSize() {
		int aimS = ++curAimSize;
		SetAimSize(aimS);
	}

	public void SetAimSize(int size) {
		curAimSize = size;
		if (curAimSize > (int)AimSize.Big)
			curAimSize = 0;
		aimSize = (AimSize)curAimSize;
	}

	public void NextAimImage() {
		int im = ++curAimImage;
		SetAimImage(im);
	}

	public void SetAimImage(int im) {
		if (im >= aimImages.Length) {
			curAimImage = 0;
		} else {
			curAimImage = im;
		}
	}

	private void OnValidate() {
		qualityLevels = QualitySettings.names;
	}
}

public enum AimSize {
	Small = 0,
	Medium = 1,
	Big = 2
}

[Serializable]
public class SaveSettingClass {
	public float x;
	public float y;
	public bool isMusic;
	public float volume;
	public int curAimSize = 0;
	public AimSize aimSize = AimSize.Small;
	public int curAimImage = 0;

	public bool isShadow = true;
	public int curQuality = 0;

	public int curResolution = 0;
	public bool isFullScreen = true;
	public bool isAF = true;
	public int curAntiAlias = 2;
	public bool isVSync = true;
}
