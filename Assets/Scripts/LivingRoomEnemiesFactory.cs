using UnityEngine;
public class LivingroomEnemiesFactory : MonoBehaviour, SpawnerFactory
{
    [SerializeField] private GameObject greenFoamPrefab; // Asigna prefab específico de Livingroom
    [SerializeField] private GameObject blueFoamPrefab;

    public BaseEnemy CreateGreenFoam(Vector3 position)
    {
        GameObject instance = Instantiate(greenFoamPrefab, position, Quaternion.identity);
        return instance.GetComponent<BaseEnemy>();
    }
    public BaseEnemy CreateBlueFoam(Vector3 position)
    {
        GameObject instance = Instantiate(blueFoamPrefab, position, Quaternion.identity);
        return instance.GetComponent<BaseEnemy>();
    }
}