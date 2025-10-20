using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    [Header("general")]
    public CinemachineCamera virtualCamera;

    public Collider2D oldConfiner;
    public Collider2D cameraConfiner;
    public GameObject[] doors;
    public Transform[] spawnPoints;

    [Header("oleadas (burbuja o verde)")]
    public List<WaveConfig> waves;
    private int currentWave = 0;
    private bool roomActive = false;
    private bool completed = false;

    private List<GameObject> currentEnemies = new List<GameObject>();
    public GameObject key;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !roomActive)
        {
            roomActive = true;
            StartCoroutine(StartRoom());
        }
    }

    IEnumerator StartRoom()
    {
        if (virtualCamera != null && cameraConfiner != null)
        {
            oldConfiner = virtualCamera.GetComponent<CinemachineConfiner2D>().BoundingShape2D;
            var confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
                confiner.BoundingShape2D = cameraConfiner;
        }

        foreach (var d in doors) d.SetActive(true);


        yield return StartCoroutine(HandleWaves());
    }

    IEnumerator HandleWaves()
    {
        yield return new WaitForSeconds(3f);
        while (currentWave < waves.Count)
        {
            yield return StartCoroutine(SpawnWave(waves[currentWave]));

            yield return new WaitUntil(() => EnemiesCleared());
            currentWave++;

            yield return new WaitForSeconds(1f);
        }

        key.SetActive(true);

        yield return new WaitUntil(() => KeyStolen());

        key.SetActive(false);

        RoomCompleted();
    }

    IEnumerator SpawnWave(WaveConfig wave)
    {
        currentEnemies.Clear();
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject prefab = wave.GetRandomEnemyPrefab();
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(prefab, point.position, Quaternion.identity);
            currentEnemies.Add(enemy);
            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }

    private bool EnemiesCleared()
    {
        currentEnemies.RemoveAll(e => e == null);
        return currentEnemies.Count == 0;
    }

    private bool KeyStolen()
    {
        KeyCollision keyCollision = key.GetComponent<KeyCollision>();
        return keyCollision.stolen;
    }

    void RoomCompleted()
    {
        if (completed) return;
        completed = true;



        foreach (var d in doors) d.SetActive(false);

        if (virtualCamera != null)
        {
            var confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
                confiner.BoundingShape2D = oldConfiner;
        }
    }
}
