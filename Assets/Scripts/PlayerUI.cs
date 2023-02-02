using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private RectTransform thrusterFuelFill;
    private PlayerController controller;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject scoreBoard;

    public void SetController(PlayerController _controller)
    {
        controller = _controller;
    }

    private void Update()
    {
        SetFuelAmount(controller.GetThrusterFuelAmount());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreBoard.SetActive(true);
        }else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreBoard.SetActive(false);
        }
    }

    private void Start()
    {
        PauseMenu.isOn = false;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }

    void SetFuelAmount(float _amount)
    {
        thrusterFuelFill.localScale =  new Vector3(1f, _amount, 1f);    
    }
}
