using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSceneFC : MonoBehaviour
{
	[SerializeField] Transform goalPoint;
	private static DefenseSceneFC instance;
	public static DefenseSceneFC Instance
	{
		get { return instance; }
	}
	public Transform GoalPoint { get { return goalPoint; } }

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
}
