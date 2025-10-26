using UnityEngine;
public interface SpawnerFactory
{
    BaseEnemy CreateGreenFoam(Vector3 position); 
    BaseEnemy CreateBlueFoam(Vector3 position);
    
}