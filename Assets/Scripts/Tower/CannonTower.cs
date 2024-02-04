using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower<TowerData.CannonTowerStat>
{
	string cannonPath = "Prefab/Tower/CannonBall";
	protected override void SetDataList()
	{
		statList = GameManager.Resource.Load<TowerData>("Data/TowerData").CannonTowerList;
	}

	protected override IEnumerator CoAttack()
	{
		while (true)
		{
			GameObject minObj = GetMinDistEnemy(statList[level].attackRange);

			if (minObj != null)
			{
				Shot(minObj.GetComponent<EnemyAction>());
			}
			yield return new WaitForSeconds(statList[level].attackDelay);
		}
	}

	private void Shot(EnemyAction action)
	{
		CannonBall ball = GameManager.Resource.Instantiate<CannonBall>(cannonPath, transform.position, Quaternion.identity, true);
		ball.Init(action.transform.position, statList[level].attackDamage, 
			statList[level].projectileSpeed, statList[level].cannonExplosionRange, level);
	}
}
