using UnityEngine;

public class AnxietyController : MonoBehaviour
{
    // Reference to the Animator
    public Animator animator;

    // Name of the Animator's boolean parameter
    private string isAnxious = "isAnxious";  // Change this to the name of your bool parameter in the Animator

    // Method called when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger");
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            // Set the boolean parameter to false
            animator.SetBool(isAnxious, true);
            Debug.Log("Player entered the trigger, Animator bool set to false.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the trigger has the tag "Player"
        if (other.CompareTag("Player") && animator != null)
        {
            // Set the boolean parameter to true
            animator.SetBool(isAnxious, false);
            Debug.Log("Player exited the trigger, Animator bool set to true.");
        }
    }
}
