using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {
	public static Setting instance;

	public float x = 1f;
	public float y = 1f;
	public bool isMusic;

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

	void Start() {
		instance = this;
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
		source.volume = volume;
	}

	public void SetResolution() {
		curResolution++;
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

	public void SetQualityLevel() {
		curQuality++;
		if (curQuality >= qualityLevels.Length)
			curQuality = 0;
		QualitySettings.SetQualityLevel(curQuality, true);
	}

	public void SetAnisotropicFiltering(bool state) {
		isAF = state;
		QualitySettings.anisotropicFiltering = isAF ? AnisotropicFiltering.ForceEnable : AnisotropicFiltering.Disable;
	}

	public void SetAntiAlias() {
		if (curAntiAlias == 0)
			curAntiAlias = 2;
		curAntiAlias *= 2;
		if (curAntiAlias == 16)
			curAntiAlias = 0;
		QualitySettings.antiAliasing = curAntiAlias;
	}

	public void SetVSync(bool state) {
		isVSync = state;
		QualitySettings.vSyncCount = isVSync ? 1 : 0;
	}

	public void SetAimSize() {
		curAimSize++;
		if (curAimSize > (int)AimSize.Big)
			curAimSize = 0;
		aimSize = (AimSize)curAimSize;
	}

	public void SetAimImage() {
		curAimImage++;
		if (curAimImage >= aimImages.Length) {
			curAimImage = 0;
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
