using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
	List<TowerData.CannonTowerStat> cannonStatList;
	protected override void SetDataList()
	{
		cannonStatList = GameManager.Resource.Load<TowerData>("Data/TowerData").CannonTowerList;
		baseStatList = cannonStatList;
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

	private void Shot(EnemyAction action)
	{

	}
}
