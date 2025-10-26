using UnityEngine;
public abstract class InteractiveItem : MonoBehaviour
{
    
    protected abstract void Interact(GameObject interactor);

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            Interact(other.gameObject);
        }
    }
}