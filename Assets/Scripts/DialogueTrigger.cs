using cherrydev;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    [SerializeField] private PlayUI playUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int maxPlayTimes = 100;
    [SerializeField] private float newEnergyJump = 0f;
    [SerializeField] private float newJumpForce = 0f;
    private int timesPlayed = 0;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dialogBehaviour != null && (timesPlayed < maxPlayTimes))
        {
            playerController.StopMovement();
            dialogBehaviour.StartDialog(dialogGraph);
            
            if (newEnergyJump > 0)
                playerController.gameObject.GetComponent<EnergyController>().setNewEnergyPerJump(newEnergyJump);

            if (newJumpForce > 0)
                playerController.SetJumpForce(newJumpForce);

            if (playUI != null)            
                playUI.HideAllControls();
            

            timesPlayed++;
        }
    }

    public void OnDialogueFinished()
    {
        playerController.AllowMovement();
        if (playUI != null)
           playUI.ShowAllControls();
        
    }

}
