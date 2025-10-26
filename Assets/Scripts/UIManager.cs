using UnityEngine;
public class UIManager : MonoBehaviour, GameManagerObserver
{
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuGameOver;   
    void Start()
    {

        GameManager gm = FindObjectOfType<GameManager>(); 
        if (gm != null)
        {
            gm.OnGameStart += OnGameStart;
            gm.OnGameOver += OnGameOver;
        }
      
        if (menuPrincipal != null) menuPrincipal.SetActive(true);
        if (menuGameOver != null) menuGameOver.SetActive(false);
    }

    public void OnGameStart()
    {
        if (menuPrincipal != null) menuPrincipal.SetActive(false);
        if (menuGameOver != null) menuGameOver.SetActive(false);
    
    }

    public void OnGameOver()
    {
        if (menuGameOver != null) menuGameOver.SetActive(true);
    }

    private void OnDestroy()
    {
       
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.OnGameStart -= OnGameStart;
            gm.OnGameOver -= OnGameOver;
        }
    }
}