// KitchenEnemiesFactory.cs
using UnityEngine;

// Esta es tu Fábrica CONCRETA. Sabe qué prefabs específicos usar.
public class KitchenEnemiesFactory : MonoBehaviour, SpawnerFactory
{
    [Header("Prefabs de la Cocina")]
    public GameObject kitchenBlueFoamPrefab;  // Arrastra tu prefab KitchenBlueFoam aquí
    public GameObject kitchenGreenFoamPrefab; // Si tienes uno, si no, déjalo vacío
    

    public GameObject GetBlueFoamPrefab()
    {
        return kitchenBlueFoamPrefab;
    }

    public GameObject GetGreenFoamPrefab()
    {
        return kitchenGreenFoamPrefab;
    }

}