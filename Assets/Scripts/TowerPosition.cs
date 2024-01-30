using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
	private Transform buildTrans;
	private bool isBuild;

	private void Awake()
	{
		isBuild = false;
		buildTrans = transform.GetChild(0);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if(isBuild == false)
		{
			BuildingManager.Instance.Build();
			isBuild = true;
		}
		else
		{
			Destroy(buildTrans.GetChild(0).gameObject);
			isBuild = false;
			OnPointerEnter(eventData);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        if (isBuild == false)
		{
			BuildingManager.Instance.MouseOnPosition(buildTrans);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        if (isBuild == false)
		{
			BuildingManager.Instance.MouseOffPosition();
		}
	}
}
