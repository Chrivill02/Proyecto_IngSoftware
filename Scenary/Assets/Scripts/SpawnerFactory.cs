// SpawnerFactory.cs
using UnityEngine;

// Basado en tu diagrama (k7ps3nBm_veq2WR3OzAD-33)
// Esta es la interfaz abstracta
public interface SpawnerFactory
{
    // Usamos "Get...Prefab" para que la fábrica solo nos diga QUÉ instanciar,
    // y el EnemyRoom se encargue de DÓNDE instanciarlo.
    GameObject GetBlueFoamPrefab();
    GameObject GetGreenFoamPrefab();
}