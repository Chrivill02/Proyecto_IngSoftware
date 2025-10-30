// WaveConfig.cs
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveConfig
{
    public string waveName;
    // --- MODIFICADO ---
    // Ya no guardamos prefabs, solo los tipos que queremos
    public List<EnemyType> enemyTypes;
    // --- FIN MODIFICACIÓN ---
    public int enemyCount = 5;
    public float spawnDelay = 0.5f;

    // --- MODIFICADO ---
    public EnemyType GetRandomEnemyType()
    {
        if (enemyTypes == null || enemyTypes.Count == 0)
        {
            Debug.LogError("¡WaveConfig no tiene tipos de enemigos asignados!");
            return default;
        }
        return enemyTypes[Random.Range(0, enemyTypes.Count)];
    }
    // --- FIN MODIFICACIÓN ---
}