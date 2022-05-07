using UnityEngine;

public class EnemyShooter : Enemy {
	public GameObject bullet;
	public Transform muzzle;
	public AudioSource source;

	public override void Attack() {
		GameObject spawnBul = Instantiate(bullet, muzzle.position, Quaternion.identity);
		source.Play();
		spawnBul.transform.forward = Player.instance.transform.position + Vector3.up - muzzle.position;
	}

	protected override void AttackProc() {
		transform.forward = Player.instance.transform.position - transform.position;
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
