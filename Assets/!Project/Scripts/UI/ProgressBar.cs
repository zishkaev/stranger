using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	public Image bar;
	public TextMeshProUGUI text;

	public void SetValue(float value, float maxValue) {
		bar.fillAmount = value / maxValue;
		text.text = ((int)value).ToString();
	}
}
