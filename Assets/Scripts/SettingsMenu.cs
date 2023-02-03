using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] Dropdown resolutionDropDown;
    [SerializeField] GameObject settingsWindow;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();  
        int currentResolutionIndex = 0; 
        for (int i = 0; i < resolutions.Length; i++)
        {
           string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i; 
            }
        }

        resolutionDropDown.AddOptions(options); 
        resolutionDropDown.value = currentResolutionIndex;   
        resolutionDropDown.RefreshShownValue(); 
    }
 
    public void SetFullScreen(bool isFullScreen)
    {
        
            Screen.fullScreen = isFullScreen;
          
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
  

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
}
