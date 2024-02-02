using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
	private NavMeshAgent agent;
	private EnemyAction action;
	private Transform goal;

	private void Awake()
	{
		action = GetComponent<EnemyAction>();
		goal = DefenseSceneFC.Instance.GoalPoint;
		agent = GetComponent<NavMeshAgent>();
	}

	private void OnEnable()
	{
		agent.SetDestination(goal.position);
	}

	private void OnDisable()
	{
		
	}

	private void Update()
	{
		if((transform.position - goal.position).sqrMagnitude < 1f)
		{
			action.Die();
		}
	}

	public void WarpToSpawn()
	{
		agent.Warp(DefenseSceneFC.Instance.SpawnPoint.position);
	}
}
