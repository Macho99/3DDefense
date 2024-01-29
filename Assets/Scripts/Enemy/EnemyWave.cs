using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveData
{
	public float startTime;
	public int spawnCount;
	public EnemySpawner spawner;
}

public class EnemyWave : MonoBehaviour
{
	[SerializeField] List<WaveData> waveDataList;

	private void Start()
	{
		foreach (WaveData waveData in waveDataList)
		{
			StartCoroutine(CoWave(waveData));
		}
	}

	private IEnumerator CoWave(WaveData data)
	{
		yield return new WaitWhile(() => Time.timeSinceLevelLoad < data.startTime);

		data.spawner.StartCoroutine(data.spawner.CoSpawn(data.spawnCount));
	}
}
