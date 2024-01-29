using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField] private float attackInterval;
	[SerializeField] private float attackDist;

	LayerMask EnemyMask;

	private void Awake()
	{
		EnemyMask = LayerMask.GetMask("Enemy");
	}

	private void Start()
	{
		_ = StartCoroutine(CoAttack());
	}

	private IEnumerator CoAttack()
	{
		while(true)
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, attackDist, EnemyMask);

			float minSqrDist = attackDist * attackDist + 9999f;
			GameObject minObj = null;
			foreach (Collider collider in colliders)
			{
				float sqrMag = (collider.transform.position - transform.position).sqrMagnitude;
				if (sqrMag < minSqrDist)
				{
					minSqrDist = sqrMag;
					minObj = collider.gameObject;
				}
			}

			if(minObj != null)
			{
				minObj.GetComponent<EnemyAction>().TakeDamage(50);
			}
			yield return new WaitForSeconds(attackInterval);
		}
	}
}
