using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Tower")]
public class TowerData : ScriptableObject
{
	[SerializeField] private List<ArcherTowerStat> archerTowerList;
	[SerializeField] private List<CannonTowerStat> cannonTowerList;

	public List<ArcherTowerStat> ArcherTowerList { get { return archerTowerList; } }
	public List<CannonTowerStat> CannonTowerList { get { return cannonTowerList; } }

	[Serializable]
	public class BaseTowerStat
	{
		public MeshRenderer mesh;
		public MeshRenderer constructionMesh;
		public float constructionTime;
	}

	[Serializable]
	public class ArcherTowerStat : BaseTowerStat
	{
		public int attackDamage;
		public float attackDelay;
		public float attackRange;
		public float projectileSpeed;
	}

	public class CannonTowerStat : BaseTowerStat
	{
		public int attackDamage;
		public float attackDelay;
		public float attackRange;
		public float cannonExplosionRange;
		public float projectileSpeed;
	}
}