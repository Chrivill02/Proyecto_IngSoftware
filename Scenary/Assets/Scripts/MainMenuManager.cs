using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; 
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Componentes de Audio")]
    public AudioSource sfxSource;   
    public AudioClip clickSound;  

    [Header("Controles de Configuración")]
    public Slider volumeSlider;  
    public Toggle vibrationToggle;

    void Awake()
    {
        
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        AudioListener.volume = savedVolume; 

        
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

       
        if (vibrationToggle != null)
        {
            
            int vibrationSetting = PlayerPrefs.GetInt("VibrationEnabled", 1);
            vibrationToggle.isOn = (vibrationSetting == 1);
        }
    }

    public void SetMasterVolume(float volume)
    {
        
        AudioListener.volume = volume;

        
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save(); 
    }

    public void ToggleVibration(bool isEnabled)
    {
        
        int vibrationValue = isEnabled ? 1 : 0;

        
        PlayerPrefs.SetInt("VibrationEnabled", vibrationValue);
        PlayerPrefs.Save();
    }

    public void PlayClickSound()
    {
       
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
    public void CargarNivel(string nombreDelNivel)
    {
        
        SceneManager.LoadScene(nombreDelNivel);
    }

    
    public void SalirDelJuego()
    {
        
        Debug.Log("¡Saliendo del juego!"); 
        Application.Quit();
    }
}