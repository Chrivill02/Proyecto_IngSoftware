using UnityEngine;
public abstract class PlayerShoot_Base : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate = 0.5f;
    protected float nextFireTime = 0f;

    // Podríamos mover el Input a un script de PlayerInput
    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    protected virtual void Shoot()
    {
        Proyectil proyectil = CrearProyectil(); // Llama al Factory Method
        if (proyectil != null)
        {
            float direction = transform.lossyScale.x > 0 ? 1f : -1f;
            proyectil.Initialize(direction);
        }
    }
    // Factory Method abstracto
    protected abstract Proyectil CrearProyectil();
}