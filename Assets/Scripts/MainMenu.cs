using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsWindow;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject quitButton;

    public void StartGame()
    {
        settingsButton.SetActive(false);    
        quitButton.SetActive(false);    
    }
    public void SettingsButton()
    {
        SettingsWindow.SetActive(true); 
    }
    public void QuitButton()
    {
        Application.Quit(); 
    }
    public void CloseSettingsWindow()
    {
        SettingsWindow.SetActive(false);
    }

   
}
