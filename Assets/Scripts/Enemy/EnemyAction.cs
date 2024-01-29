using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
	[SerializeField] int maxHp;
	private int curHp;

	public int CurHp { get { return curHp; } }

	private void Awake()
	{
		curHp = maxHp;
	}

	private void OnDisable()
	{
		curHp = maxHp;
	}

	public void TakeDamage(int damage)
	{
		if (curHp <= 0) return;

		curHp -= damage;
		if(curHp <= 0)
		{
			curHp = 0;
			Die();
		}
	}

	private void Die()
	{
		LeanPool.Despawn(gameObject);
	}
}
