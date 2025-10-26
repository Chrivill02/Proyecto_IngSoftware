using System;
using UnityEngine;
public abstract class BaseEnemy : MonoBehaviour, Damageable
{
    [SerializeField] protected int vidaInicial = 3;
    protected int vidaActual;
    public event Action<BaseEnemy> OnMuerte; 

    protected virtual void Awake()
    { 
        vidaActual = vidaInicial;
    }

    public virtual void RecibirDano(int cantidad)
    {
        if (vidaActual <= 0) return;
        vidaActual -= cantidad;
        
        Debug.Log($"{gameObject.name} recibió {cantidad} daño, vida restante: {vidaActual}");
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    protected virtual void Morir()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        OnMuerte?.Invoke(this); 
        GetComponent<Collider2D>().enabled = false;
        
        this.enabled = false;
        Destroy(gameObject, 2f); // Destruir después de un tiempo
    }
}