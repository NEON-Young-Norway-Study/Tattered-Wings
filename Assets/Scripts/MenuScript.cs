using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene("TJRoom");
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
