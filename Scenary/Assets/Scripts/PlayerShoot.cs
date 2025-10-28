using UnityEngine;





public class PlayerShoot : MonoBehaviour, PlayerHabilitiesInputObserver
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;


    void Start()
    {
        PlayerInputManager inputManager = FindFirstObjectByType<PlayerInputManager>();
        inputManager.OnShootKeyPressed += OnShootKeyPressed;
    }

    public void OnShootKeyPressed()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        float direction = Mathf.Sign(transform.localScale.x);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (direction < 0)
        {
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x *= -1; // invierte el sprite
            bullet.transform.localScale = bulletScale;
        }

        bullet.GetComponent<Projectile>().SetDirection(direction);

    }
}