using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        float muted = PlayerPrefs.GetInt("muted", 1);
        SetVolume(volume);
        volumeSlider.value = volume;
        
        if (muted == 0)
        {
            muteToggle.isOn = true;
        }   
        else
        {
            muteToggle.isOn = false;
        }

    }

    // Scene Management

    public void LoadGame()
    {
        SceneManager.LoadScene(1); // 0 is Menu; 1 is Game
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void SetMute(bool mute)
    {
        Debug.Log("mute =" + mute);
        FindObjectOfType<SFXController>().MuteHandler(mute);
    }

    public void SetVolume(float volume)
    {
        FindObjectOfType<SFXController>().VolumeHandler(volume);
    }

}
