using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
	[SerializeField] private Material pointerEnterMaterial;

	private TowerSelect towerSelect;
	private Material baseMaterial;
	private Renderer flagRenderer;
	private Transform buildTrans;

	private void Awake()
	{
		flagRenderer = GetComponentInChildren<Renderer>();
		baseMaterial = flagRenderer.material;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		GameManager.UI.ShowInGameUI<TowerSelect>("UI/TowerSelect").SetTarget(transform);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		flagRenderer.material = pointerEnterMaterial;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		flagRenderer.material = baseMaterial;
	}
}