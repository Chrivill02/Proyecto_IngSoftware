// FoamEnemy.cs
using UnityEngine;

// Hereda de ChaserEnemy
public abstract class FoamEnemy : ChaserEnemy
{
    // Aquí puedes poner la lógica común a TODAS las espumas.
    // Por ejemplo, el sonido de "pop" al morir que tenías en BlueFoam.
    public override void Die()
    {
        // ...ejecutar sonido de "pop"...
        base.Die(); // Llama al Destroy(gameObject) de ChaserEnemy
    }
}