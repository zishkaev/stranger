using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public float tShowing = 1f;

    public Animator anim;

    public Action onEnd;

    public static ScreenFade instance;

	private void Awake() {
        instance = this;
	}

	public void Show() {
        anim.SetTrigger("FadeIn");
	}

    public void HideLong() {
        anim.SetTrigger("FadeOutLong");
	}

    public void Hide() {
        anim.SetTrigger("FadeOut");
    }

    public void OnEnd() {
        onEnd?.Invoke();
	}
}
