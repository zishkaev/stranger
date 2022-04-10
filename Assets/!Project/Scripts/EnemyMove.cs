using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour {
	public NavMeshAgent navMeshAgent;
	private Transform player;
	private bool move;

	private void Start() {
		player = Player.instance.transform;
	}

	private void Update() {
		if (move)
			navMeshAgent.SetDestination(player.position);
	}

	public void SetMove(bool state) {
		move = state;
		navMeshAgent.isStopped = !state;
	}
}
