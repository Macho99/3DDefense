using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipUI : PopUpUI
{
	[SerializeField] Camera tooltipCam;
	[SerializeField] Vector3 camOffset;

	private Transform target;
	Coroutine TraceCoroutine;

	public void Set(EnemyAction action)
	{
		target = action.transform;
		gameObject.SetActive(true);
	}
}
