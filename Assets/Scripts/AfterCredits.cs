using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Xasu.HighLevel;

public class AfterCredits : MonoBehaviour
{


    public Animator animator;
    public string animationName; // Nombre del estado de animación que quieres comprobar
    InputAction touchAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        touchAction = InputSystem.actions.FindAction("TouchOrClick");
        //touchAction.performed += ctx => LoadEnd();
    }

    // Update is called once per frame
    private void LoadEnd()
    {
        CompletableTracker.Instance.Completed("Tattered-wings", CompletableTracker.CompletableType.Game);
        SceneManager.LoadScene("Continue");
    }

    void Update()
    {
        // Obtener la información del estado actual del Animator en la capa 0
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Verificar si el estado actual corresponde a la animación deseada
        if (stateInfo.IsName(animationName))
        {
            // Verificar si la animación ha terminado (tiempo normalizado >= 1)
            if (stateInfo.normalizedTime >= 1f && touchAction.WasPressedThisFrame())
            {
                
                LoadEnd();
            }
        }
    }
}
