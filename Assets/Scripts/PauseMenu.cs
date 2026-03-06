using TMPro;
using UnityEngine;
using Xasu.HighLevel;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayUI playUI;
    [SerializeField] private GameObject baseOptions;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private TMP_Dropdown stickDirection;
    //[SerializeField] private DropdownSample 

    private Canvas canvas;

    void Start()
    {
        //gameObject.SetActive(false);
        optionsMenu.SetActive(false);
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        if (playUI == null)
        {
            Debug.LogWarning("PlayUI is not set in the inspector");
        }
    }

    public void OnResumeButtonPressed()
    {
        //Time.timeScale = 1;
        PauseGame.isPaused = false;
        canvas.enabled = false;
        //gameObject.SetActive(false);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void OnOptionsButtonPressed()
    {
        baseOptions.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        baseOptions.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        AccessibleTracker.Instance.Accessed("PauseMenu", AccessibleTracker.AccessibleType.Screen);
        canvas.enabled = true;
        PauseGame.isPaused = true;
    }

    public void OnJoystickDirectionChanged(int newVal)
    {
        var dropdownValue = stickDirection.value;
        if (playUI == null)
        {
            Debug.LogWarning("PlayUI is not set in the inspector");
            return;
        }
        if (dropdownValue == 0)
        {
            playUI.SetJoystickLeft();
        } 
        else if (dropdownValue == 1)
        {
            playUI.SetJoystickRight();
        }
        else
        {
            playUI.SetJoystickRight();
        }
    }
}
