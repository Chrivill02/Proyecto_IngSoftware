using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject blackScreen; 
    public float delay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(Finish());
        }
    }

    private System.Collections.IEnumerator Finish()
    {
        blackScreen.SetActive(true); // Activar pantalla negra
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia nivel
    }
}
