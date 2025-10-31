using UnityEngine;

public class CameraBounds : MonoBehaviour
{
   
    public Transform target;

    // La velocidad con la que la cámara sigue al objetivo
    public float smoothing = 5f;

    // Los límites del mapa
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (target != null)
        {
            
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

            
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}