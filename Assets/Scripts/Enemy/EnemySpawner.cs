using System.Collections;
using UnityEngine;
using Lean.Pool;
public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] float spawnInterval;
	[SerializeField] Transform spawnPoint;

	private Transform enemyFolder;

	private void Awake()
	{
		enemyFolder = new GameObject("Enemy").transform;
	}

	public IEnumerator CoSpawn(int maxCount)
	{
		int curSpawn = 0;
		while(curSpawn < maxCount)
		{
			LeanPool.Spawn(enemyPrefab, spawnPoint.position, spawnPoint.rotation, enemyFolder);
			curSpawn++;
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
