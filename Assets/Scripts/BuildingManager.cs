using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	public static BuildingManager Instance;
	public Tower towerPrefab;
	public GameObject towerIndicaterPrefab;
	public Transform targetTrans;

	private GameObject currentIndicater;

	private void Awake()
	{
		Instance = this;
		currentIndicater = Instantiate(towerIndicaterPrefab, Vector3.zero, Quaternion.identity);
		currentIndicater.SetActive(false);
	}

	private void Update()
	{
		if(targetTrans != null)
		{
			currentIndicater.transform.position = targetTrans.position;
			currentIndicater.SetActive(true);
		}
		else
		{
			currentIndicater.SetActive(false);
		}
	}

	public void MouseOnPosition(Transform target)
	{
		targetTrans = target;
	}

	public void MouseOffPosition()
	{
		targetTrans = null;
	}

	public void Build()
	{
		Instantiate(towerPrefab, targetTrans.position, Quaternion.identity, targetTrans);
	}
}
