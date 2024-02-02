using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArcherTower : Tower
{
	[SerializeField] private Transform shotPoint;

	string arrowPath = "Prefab/Tower/Arrow";

	protected override void SetDataList()
	{
		baseStatList = GameManager.Resource.Load<TowerData>("Data/TowerData").ArcherTowerList;
	}

	protected override IEnumerator CoAttack()
	{
		while (true)
		{
			GameObject minObj = GetMinDistEnemy(baseStatList[level].attackRange);

			if (minObj != null)
			{
				Shot(minObj.GetComponent<EnemyAction>());
			}
			yield return new WaitForSeconds(baseStatList[level].attackDelay);
		}
	}

	private void Shot(EnemyAction target)
	{
		Arrow arrow = GameManager.Resource.Instantiate<Arrow>(arrowPath, shotPoint.position, Quaternion.identity, true);
		arrow.Init(target, baseStatList[level].attackDamage, baseStatList[level].projectileSpeed);
	}
}