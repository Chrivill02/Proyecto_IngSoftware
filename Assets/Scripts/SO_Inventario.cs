using UnityEngine;

[CreateAssetMenu(fileName = "NuevoInventario", menuName = "Inventario/InventarioData")]
public class SO_Inventario : ScriptableObject
{

    [SerializeField] private bool _tieneLlave = false; 

    public bool TieneLlave => _tieneLlave; 
    public void GetKey()
    {
        if (!_tieneLlave)
        {
            _tieneLlave = true;
            Debug.Log("Inventario: Llave obtenida!");
           
        }
    }

    public void Reset()
    {
        _tieneLlave = false;
        Debug.Log("Inventario: Reseteado.");
    }

   
}