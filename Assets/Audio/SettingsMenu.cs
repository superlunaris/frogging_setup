using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public PlayerCamera playerCamera;
    public Slider sensitivitySlider;

    //public PlayerSwinging playerSwinging;
    //public Slider aimAssistSlider;

    bool cursor = false;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);

        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        LoadSettings();
    }

    private void Update()
    {
        if (!cursor && Input.GetKeyDown(KeyCode.Escape))
        {
            settingsMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursor = true;
        }
        else if (cursor && Input.GetKeyDown(KeyCode.Escape))
        {
            settingsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursor = false;
        }
    }

    public void QuitGame()
    {

        print("player has quit!");
        Application.Quit();
    }

    void LoadSettings()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        SetVolume(volume);

        float sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        SetSensitivity(sensitivity);

        /*
        float aimAssist = PlayerPrefs.GetFloat("AimAssist");
        SetAimAssist(aimAssist);
        */
        
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
        volumeSlider.value = value;

        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetSensitivity(float value)
    {
        playerCamera.sensX = value;
        playerCamera.sensY = value;
        sensitivitySlider.value = value;

        PlayerPrefs.SetFloat("Sensitivity", value);
    }

    /*
    public void SetAimAssist(float value)
    {
        playerSwinging.predictionSphereCastRadius = value;

        PlayerPrefs.SetFloat("AimAssist", value);
    }
    */
}
