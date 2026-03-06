using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu.HighLevel;

public class MenuScript : MonoBehaviour
{
    public void OnStartGame()
    {
        AccessibleTracker.Instance.Accessed("TJRoom", AccessibleTracker.AccessibleType.Area);
        SceneManager.LoadScene("TJRoom");
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
