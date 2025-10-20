using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Dirección según la escala del jugador
        float direction = Mathf.Sign(transform.localScale.x);

        // Instanciar la bala en la posición y rotación del firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Si el jugador mira hacia la izquierda, girar visualmente la bala
        if (direction < 0)
        {
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x *= -1; // invierte el sprite
            bullet.transform.localScale = bulletScale;
        }

        // Pasar la dirección al script de la bala
        bullet.GetComponent<GlueBullet>().SetDirection(direction);
    }
}