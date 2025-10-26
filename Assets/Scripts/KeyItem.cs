using UnityEngine;
public class KeyItem : InteractiveItem
{
    [SerializeField] private SO_Inventario inventario;

    protected override void Interact(GameObject interactor)
    {
        if (inventario != null)
        {
            inventario.GetKey();
            Debug.Log("Llave recogida!");

            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Inventario SO no asignado a KeyItem!", this);
        }
    }
    void Start() { /* ... chequeo de asignación del inventario ... */ }
}