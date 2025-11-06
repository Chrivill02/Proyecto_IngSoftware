using UnityEngine;
using UnityEngine.Audio; 

public class VolumeLoader : MonoBehaviour
{

    void Awake()
    {

        AudioListener.volume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
    }
}