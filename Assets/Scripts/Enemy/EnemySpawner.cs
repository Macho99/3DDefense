using System.Collections;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] float spawnInterval;

	private Transform spawnPoint;
	private Transform enemyFolder;

	private void Awake()
	{
		enemyFolder = new GameObject("Enemy").transform;
	}

	private void Start()
	{
		spawnPoint = DefenseSceneFC.Instance.SpawnPoint;
	}

	public IEnumerator CoSpawn(int maxCount)
	{
		int curSpawn = 0;
		while(curSpawn < maxCount)
		{
			_ = GameManager.Resource.Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, enemyFolder, true);
			curSpawn++;
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
