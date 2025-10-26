using UnityEngine;
using System.Collections.Generic; // Para lista de minions
using System;

public class DiningroomBoss : BaseEnemy, SpawnerFactory
{
    // ... (Variables de vida, movimiento, ataque del Boss) ...

    [Header("Factory Settings")]
    [SerializeField] private GameObject greenFoamMinionPrefab;
    [SerializeField] private GameObject blueFoamMinionPrefab;
    [SerializeField] private Transform spawnPoint; // Dónde spawnea minions

    [Header("Minion Management")]
    [SerializeField] private int maxMinions = 3;
    private List<BaseEnemy> activeMinions = new List<BaseEnemy>();

    public BaseEnemy CreateGreenFoam(Vector3 position)
    { // Crea sus propios tipos de espuma
        GameObject instance = Instantiate(greenFoamMinionPrefab, position, Quaternion.identity);
        BaseEnemy minion = instance.GetComponent<BaseEnemy>();
        if (minion != null) RegisterMinion(minion);
        return minion;
    }
    public BaseEnemy CreateBlueFoam(Vector3 position)
    {
        GameObject instance = Instantiate(blueFoamMinionPrefab, position, Quaternion.identity);
        BaseEnemy minion = instance.GetComponent<BaseEnemy>();
        if (minion != null) RegisterMinion(minion);
        return minion;
    }
    public void SpawnMinion()
    { // Decide cuál crear y dónde
        if (!CanSpawn()) return;
        Debug.Log("Boss spawneando minion...");
        
        if (UnityEngine.Random.value > 0.5f)
        {
            CreateGreenFoam(spawnPoint.position);
        }
        else
        {
            CreateBlueFoam(spawnPoint.position);
        }
    }

    private void RegisterMinion(BaseEnemy minion)
    {
        if (!activeMinions.Contains(minion))
        {
            activeMinions.Add(minion);
            minion.OnMuerte += OnMinionEliminated;
        }
    }

    private void OnMinionEliminated(BaseEnemy minion)
    {
        Debug.Log("Boss: Minion eliminado.");
        if (minion != null)
        {
            minion.OnMuerte -= OnMinionEliminated; 
            activeMinions.Remove(minion);
        }
        
    }

    private bool CanSpawn() { return activeMinions.Count < maxMinions ; }
    private bool HasActiveMinions() { return activeMinions.Count > 0; }

    // --- Lógica del Boss (Update, FixedUpdate, Estados, Ataques) ---
    // ... (Implementar la máquina de estados y comportamiento del jefe) ...

    protected override void Morir()
    {
        // Lógica específica de muerte del jefe (ej. limpiar minions restantes)
        foreach (var minion in activeMinions.ToArray()) // Usar ToArray para evitar problemas al modificar la lista
        {
            if (minion != null) Destroy(minion.gameObject);
        }
        activeMinions.Clear();
        base.Morir(); // Llama a la lógica base (evento, destrucción)
    }
}