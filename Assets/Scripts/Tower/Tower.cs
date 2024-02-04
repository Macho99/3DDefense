using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler 
	where T : TowerData.BaseTowerStat
{
	protected MeshRenderer meshRenderer;
	protected bool isConstructing;
	protected List<T> statList;
	protected int level = -1;

	LayerMask enemyMask;
	Coroutine attackCoroutine;

	protected abstract void SetDataList();

	protected virtual void Awake()
	{
		SetDataList();
		isConstructing = false;
		enemyMask = LayerMask.GetMask("Enemy");
		meshRenderer = GetComponentInChildren<MeshRenderer>();

		LevelUp();
	}

	private void LevelUp()
	{

		level++;
		Destroy(meshRenderer.gameObject);
		meshRenderer = Instantiate(statList[level].constructionMesh, transform.position, Quaternion.identity, transform);
		if(attackCoroutine != null)
		{
			StopCoroutine(attackCoroutine);
		}
		_ = StartCoroutine(CoConstruction(statList[level].constructionTime));
	}
	

	public virtual void OnPointerEnter(PointerEventData eventData)
	{

	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isConstructing == true) return;
		LevelUp();
	}

	protected abstract IEnumerator CoAttack();

	protected GameObject GetMinDistEnemy(float range)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, range, enemyMask);

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

		return minObj;
	}

	protected IEnumerator CoConstruction(float time)
	{
		isConstructing = true;
		yield return new WaitForSeconds(time);
		Destroy(meshRenderer.gameObject);
		meshRenderer = Instantiate(statList[level].mesh, transform.position, Quaternion.identity, transform);
		attackCoroutine = StartCoroutine(CoAttack());
		isConstructing = false;
	}
}