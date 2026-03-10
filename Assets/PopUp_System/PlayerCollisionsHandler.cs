using UnityEngine;

public class PlayerCollisionsHandler : MonoBehaviour
{
    public PopUpManager popUpManager; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            if (popUpManager != null)
            {
                popUpManager.CreatePopUp();
            }
            else
            {
                Debug.LogWarning("PopUpManager not assigned in Inspector.");
            }
        }
    }
}
