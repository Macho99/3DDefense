using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerAttack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Arrow arrowPrefab;
	[SerializeField] Transform shotPoint;
	[SerializeField] private float attackInterval = 1f;
	[SerializeField] private float attackRange = 40f;
	[SerializeField] private float arrowSpeed = 5f;
	[SerializeField] private int arrowDamage = 10;

	bool isPointerOver;
	LayerMask enemyMask;

	private void Awake()
	{
		isPointerOver = false;
		enemyMask = LayerMask.GetMask("Enemy");
	}

	private void Start()
	{
		_ = StartCoroutine(CoAttack());
	}

	private IEnumerator CoAttack()
	{
		while(true)
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, enemyMask);

			float minSqrDist = 99999f;
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
				Shot(minObj.GetComponent<EnemyAction>());
			}
			yield return new WaitForSeconds(attackInterval);
		}
	}

	private void Shot(EnemyAction target)
	{
		Arrow arrow = LeanPool.Spawn(arrowPrefab, shotPoint.position, Quaternion.identity);
		arrow.Init(target, arrowDamage, arrowSpeed);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;
	}

}