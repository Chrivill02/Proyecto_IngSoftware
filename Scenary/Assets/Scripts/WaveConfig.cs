using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveConfig
{
    public string waveName;
    public List<GameObject> enemyPrefabs;
    public int enemyCount = 5;
    public float spawnDelay = 0.5f;

    public GameObject GetRandomEnemyPrefab()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }
}
