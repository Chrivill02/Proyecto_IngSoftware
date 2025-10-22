using System.Collections;
using UnityEngine;

public class FlyingEnemySpawner : MonoBehaviour
{
    [Header("Configuración del Prefab")]
    public GameObject flyingEnemyPrefab;

    [Header("Intervalo de Aparición")]
    public float spawnInterval = 2.5f;

    [Header("Límites Verticales (Eje Y)")]
    public float minSpawnY = -2f; // <-- NUEVO: Altura mínima para aparecer
    public float maxSpawnY = 3f;  // <-- NUEVO: Altura máxima para aparecer

    [Header("Rango de Velocidad")]
    public float minSpeed = 4f; // <-- NUEVO: Velocidad mínima del enemigo
    public float maxSpeed = 8f; // <-- NUEVO: Velocidad máxima del enemigo

    [Header("Configuración de Pantalla")]
    public float screenPadding = 2f;

    private bool isSpawning = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            int side = Random.Range(0, 2);
            float camWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;

            // --- LÓGICA MODIFICADA ---
            // Ahora usa tus variables públicas para la altura Y y la velocidad
            float spawnY = Random.Range(minSpawnY, maxSpawnY);
            float randomSpeed = Random.Range(minSpeed, maxSpeed);

            Vector2 spawnPosition;
            int direction;

            if (side == 0) // Lado izquierdo
            {
                spawnPosition = new Vector2(mainCamera.transform.position.x - (camWidth / 2) - screenPadding, spawnY);
                direction = 1;
            }
            else // Lado derecho
            {
                spawnPosition = new Vector2(mainCamera.transform.position.x + (camWidth / 2) + screenPadding, spawnY);
                direction = -1;
            }

            GameObject enemy = Instantiate(flyingEnemyPrefab, spawnPosition, Quaternion.identity);
            FlyingEnemy flyingEnemyScript = enemy.GetComponent<FlyingEnemy>();

            if (flyingEnemyScript != null)
            {
                // Le pasamos la dirección Y la nueva velocidad aleatoria
                flyingEnemyScript.Initialize(direction, randomSpeed);
            }
        }
    }
}