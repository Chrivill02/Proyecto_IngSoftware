// ParedFalsa.cs
using UnityEngine;

// 1. Implementamos la interfaz "Damageable"
public class ParedFalsa : MonoBehaviour, Damageable
{
    public int health = 1;

    public void RecibirDano(int cantidad)
    {
        health -= cantidad;

        if (health <= 0)
        {
            DestruirPared();
        }
    }

    private void DestruirPared()
    {

        Destroy(gameObject);
    }
}