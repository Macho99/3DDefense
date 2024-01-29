using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
	private NavMeshAgent agent;
	private Transform goal;

	private void Awake()
	{
		goal = DefenseSceneFC.Instance.GoalPoint;
		agent = GetComponent<NavMeshAgent>();
	}

	private void OnEnable()
	{
		agent.SetDestination(goal.position);
	}

	private void Update()
	{
		if((transform.position - goal.position).sqrMagnitude < 1f)
		{
			LeanPool.Despawn(this);
		}
	}
}
