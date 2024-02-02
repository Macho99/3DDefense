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

	EnemyMove enemyMove;
	EnemyHpBar hpBar;

	public int CurHp { get { return curHp; } }

	private void Awake()
	{
		curHp = maxHp;
		enemyMove = GetComponent<EnemyMove>();
		OnHpChanged = new UnityEvent<float>();
	}

	private void OnEnable()
	{
		hpBar = GameManager.UI.ShowInGameUI(GameManager.Resource.Load<EnemyHpBar>("UI/EnemyHPBar"));
		hpBar.SetTarget(transform);
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

	public void Die()
	{
		enemyMove.WarpToSpawn();
		GameManager.UI.CloseInGameUI(hpBar);
		GameManager.Resource.Destroy(gameObject);
	}

	public void GetTooltipStr(out string name, out string desc)
	{
		name = enemyName;
		desc = "¸ó½ºÅÍ~";
	}
}
