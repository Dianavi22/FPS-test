using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PauseMenu : NetworkBehaviour
{
    public static bool isOn = false;
    private NetworkManager networkManager;


    public GameObject SettingsWindow;
    [SerializeField] GameObject settingsButton;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void LeaveRoomButton()
    {
        if (isClientOnly)
        {
            networkManager.StopClient();
        }
        else
        {
            networkManager.StopHost();
        }
    }

    public void SettingsButton()
    {
        SettingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        SettingsWindow.SetActive(false);
    }
}
