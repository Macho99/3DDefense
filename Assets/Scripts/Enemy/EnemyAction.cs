using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAction : MonoBehaviour
{
	[SerializeField] string enemyName;
	[SerializeField] int maxHp;
	private int curHp;

	[HideInInspector] public UnityEvent<float> OnHpChanged;

	public int CurHp { get { return curHp; } }

	private void Awake()
	{
		curHp = maxHp;
		OnHpChanged = new UnityEvent<float>();
	}

	private void OnDisable()
	{
		curHp = maxHp;
	}

	public void TakeDamage(int damage)
	{
		if (curHp <= 0) return;

		curHp -= damage;
		OnHpChanged?.Invoke(((float)curHp) / maxHp);
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

	public void GetTooltipStr(out string name, out string desc)
	{
		name = enemyName;
		desc = "¸ó½ºÅÍ~";
	}
}
