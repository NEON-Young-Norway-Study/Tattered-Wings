using Terresquall;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private RectTransform nativeJoystick;
    [SerializeField] private GameObject energySlider;
    [SerializeField] private GameObject jumpButton;
    private VirtualJoystick virtualJoystick;

    public delegate void JumpButton(); // Definir el tipo de delegado para el evento
    public static event JumpButton OnJumpButtonPressed; // El evento estático al que otros scripts pueden suscribirse


    private void Start()
    {
        virtualJoystick = nativeJoystick.GetComponent<VirtualJoystick>();
        if (pauseMenu == null) 
            Debug.LogWarning("Pause Menu is not set in the inspector");
        
    }

    public void OnPauseButtonPressed()
    {
        if (pauseMenu == null)
        {
            Debug.LogWarning("Pause Menu is not set in the inspector");
            return;
        }
        
        pauseMenu.ShowPauseMenu();  
    }

    public void HideVirtualJoystick()
    {
        nativeJoystick.gameObject.GetComponent<Image>().enabled = false;
        nativeJoystick.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void ShowVirtualJoystick()
    {
        nativeJoystick.gameObject.GetComponent<Image>().enabled = true;
        nativeJoystick.GetChild(0).GetComponent<Image>().enabled = true;
    }

    public void HideEnergySlider() 
    { 
        energySlider.gameObject.SetActive(false);
    }

    public void ShowEnergySlider()
    {
        energySlider.gameObject.SetActive(true);
    }

    public void ShowJumpButton()
    {
        jumpButton.gameObject.SetActive(true);
    }

    public void HideJumpButton()
    {
        jumpButton.gameObject.SetActive(false);
    }

    public void ShowAllControls()
    {
        ShowVirtualJoystick();
        ShowEnergySlider();
        ShowJumpButton();
    }

    public void HideAllControls()
    {
        HideVirtualJoystick();
        HideEnergySlider();
        HideJumpButton();
    }


    public void SetJoystickLeft()
    {
        virtualJoystick.enabled = false;
        nativeJoystick.anchoredPosition = new Vector2(210, 203);
        virtualJoystick.enabled = true;
    }

    public void SetJoystickRight()
    {
        virtualJoystick.enabled = false;
        nativeJoystick.anchoredPosition = new Vector2(1640, 203);
        virtualJoystick.enabled = true;
    }

    public void JumpButtonPressed()
    {
        OnJumpButtonPressed?.Invoke(); // Invocar el evento
    }
}
