using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, GameManagerObserver
{
    
    
    public GameObject menuPrincipal;
    public GameObject menuGameOver;

    void Start()
    {
        GameManager gameManagerScrips = FindAnyObjectByType<GameManager>();
        if (gameManagerScrips != null)
        {
            gameManagerScrips.OnGameOver += OnGameOver;
            gameManagerScrips.OnGameStart += OnGameStart;
        }
    }

    public void OnGameOver()
    {
        menuGameOver.SetActive(true);
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnGameStart()
    {
        menuPrincipal.SetActive(false);

    }
    
    
}
