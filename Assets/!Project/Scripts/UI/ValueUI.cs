using UnityEngine;
using TMPro;

public class ValueUI : MonoBehaviour {
	public TextMeshProUGUI text;

	public void SetValue(int value) {
		text.text = value.ToString();
	}

	public void SetValue(int value, int maxValue) {
		text.text = string.Format("{0}/{1}", value, maxValue);
	}
}
