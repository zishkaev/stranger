using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoyOnLoad : MonoBehaviour {
	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}
