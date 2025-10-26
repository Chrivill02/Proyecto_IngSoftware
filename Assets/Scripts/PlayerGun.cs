using UnityEngine;
public class PlayerGun : PlayerShoot_Base
{ 
    [Header("Concrete Projectile")]
    [SerializeField] private GameObject bulletPrefab; // Asigna aquí el prefab de GlueBullet

    protected override Proyectil CrearProyectil()
    {
        if (bulletPrefab == null || firePoint == null) { return null; }
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        return bulletGO.GetComponent<Proyectil>();
    }
    
}