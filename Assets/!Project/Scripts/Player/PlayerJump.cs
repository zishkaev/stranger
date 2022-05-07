using UnityEngine;

public class PlayerJump : MonoBehaviour {
	public Rigidbody rigidbody;
	public float jumpForce;
	public string tagGround;
	public Transform basePoint;
	private int jumpCount = 1;
	private bool checkLanding;
	private bool isActive;

	public int jumpAllCount = 1;

	public GameObject ui;
	public bool jumpProc;

	private void Start() {
		ui.SetActive(false);
		InputSystem.instance.onJump += Jump;
	}

	//private void Update() {
	//	if (!isActive) return;
	//	if (jump)
	//		CheckLanding();
	//}

	public void SetActive(bool state) {
		isActive = state;
	}

	public void AddJumpCount() {
		jumpAllCount++;
		ui.SetActive(true);
	}

	public void Jump() {
		if (jumpCount == 0) return;
		jumpCount--;
		rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		jumpProc = true;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(tagGround)) {
			jumpCount = jumpAllCount;
			jumpProc = false;
		}
	}

	public void CheckLanding() {
		Collider[] colliders = Physics.OverlapSphere(basePoint.position, 0.1f);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].CompareTag(tagGround)) {
				if (checkLanding == false)
					return;
				jumpCount = jumpAllCount;
				return;
			}
		}
		checkLanding = true;
	}

	private void OnDestroy() {
		InputSystem.instance.onJump -= Jump;
	}
}
