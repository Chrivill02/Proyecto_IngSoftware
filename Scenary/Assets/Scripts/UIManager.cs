using UnityEngine;

public class UIManager : MonoBehaviour, GameManagerObserver
{
    public GameObject menuPrincipal;
    public GameObject menuGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.OnGameStart += OnGameStart;
        gameManager.OnGameOver += OnGameOver;
    }
    
    public void OnGameStart()
    {
        menuGameOver.SetActive(false);
    }

    public void OnGameOver()
    {
        menuGameOver.SetActive(true);
    }
}
