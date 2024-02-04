using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { None, Archer, Cannon, Size }

public class TowerSelect : InGameUI
{
	TowerPosition towerPosition;

	Transform panelTrans;
	TowerType curType;

	protected override void Awake()
	{
		base.Awake();

		buttons["Blocker"].onClick.AddListener(CloseUI);
		panelTrans = transforms["Panel"];
		SetOffset(new Vector3(200f, 0f, 0f));

		buttons["ArcherTowerButton"].onClick.AddListener(() => { curType = TowerType.Archer; });
		buttons["CanonTowerButton"].onClick.AddListener(() => { curType = TowerType.Cannon;	});
		buttons["ConfirmButton"].onClick.AddListener(Confirm);
	}

	private void OnEnable()
	{
		curType = TowerType.None;
	}

	private void Confirm()
	{
		switch (curType)
		{
			case TowerType.Archer:
				GameManager.Resource.Instantiate<ArcherTower>("Prefab/Tower/ArcherTower",
					towerPosition.transform.position,
					Quaternion.identity,
					towerPosition.transform.parent
					);
				break;
			case TowerType.Cannon:
				GameManager.Resource.Instantiate<CannonTower>("Prefab/Tower/CannonTower",
					towerPosition.transform.position,
					Quaternion.identity,
					towerPosition.transform.parent
					);
				break;
			default:
				CloseUI();
				return;
		}
		CloseUI();
		Destroy(towerPosition.gameObject);
	}

	protected override void LateUpdate()
	{
		if(panelTrans != null)
		{
			panelTrans.position = Camera.main.WorldToScreenPoint(followTarget.position) + followOffset;
		}
	}

	protected override void Init()
	{
		towerPosition = followTarget.GetComponent<TowerPosition>();
	}
}
