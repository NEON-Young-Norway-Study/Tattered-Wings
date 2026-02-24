using DG.Tweening;
using Terresquall;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public delegate void JumpAction(); // Definir el tipo de delegado para el evento
    public static event JumpAction OnJump; // El evento estático al que otros scripts pueden suscribirse


    public GameObject objectToMove; // El GameObject que deseas mover
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private VirtualJoystick virtualJoystick;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private EnergyController energyController;
    [SerializeField] private Animator animator;
    private AudioSource audioSource;


    [SerializeField] private LayerMask groundLayer; // Capa que define el suelo
    [SerializeField] private float acceleration = 10f; // Aceleraci�n del movimiento
    [SerializeField] private float deceleration = 15f; // Desaceleraci�n al soltar el joystick
    [SerializeField] private float maxSpeed = 10f; // Velocidad m�xima
    [SerializeField] private float dmgDuration = 0.25f;
    [SerializeField] private int hitstopOnDamage = 60;


    InputAction jumpAction;
    InputAction moveAction;
    private Vector3 currentVelocity = Vector3.zero;
    private bool isGrounded = false;
    private int hitstopFrames = 0;

    // Duration for each color transition
    // Initial color (white)
    private Color startColor = Color.white;

    // Target color (slight red)
    private Color targetColor = new Color(1f, 152f / 255f, 152f / 255f); // Slight red


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        jumpAction = InputSystem.actions.FindAction("Jump");
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction.performed += ctx => Jump();
        PlayUI.OnJumpButtonPressed += Jump;

    }


    private void OnDestroy()
    {
        jumpAction.performed -= ctx => Jump();
        PlayUI.OnJumpButtonPressed -= Jump;
    }


    public void StartHitstop(int hitstopAmount)
    {
        hitstopFrames = hitstopAmount;
    }

    public void StopHitstop()
    {
        hitstopFrames = 0;
    }

    public void TakeDamage()
    {
        StartHitstop(hitstopOnDamage);
        rigidbody.linearVelocity = new Vector3(0f, 0f, 0f);
        energyController.SetCurrentEnergy(0f);

        /*
        // Tween from white to slight red and back to white
        spriteRenderer.material.DOColor(targetColor, dmgDuration).From(startColor)
            .SetLoops(2, LoopType.Yoyo)  // Loop back to white
            .SetEase(Ease.InOutSine);    // Optional easing function
        */
    }



    private void FixedUpdate()
    {

        //if (PauseGame.isPaused)
        //{
        //    Debug.Log("Game is paused");   
        //    return;
        //}
        //Debug.Log("is Paused: " + PauseGame.isPaused);
        if (hitstopFrames > 0)
        {
            hitstopFrames--;
            // Seguir aplicando gravedad durante el hitstop
            rigidbody.linearVelocity = new Vector3(0f, rigidbody.linearVelocity.y, 0f);
            return;
        }

        var joystickAxis = virtualJoystick.GetAxis();


        // Verificar si el personaje está en el suelo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);
        if (animator != null) animator.SetBool("IsOnAir", !isGrounded);

        if (jumpAction.WasReleasedThisFrame() && energyController.HasEnergyToFly() && !IsGamePaused())
        {
            if (animator != null) animator.SetBool("IsFlapping", true);
            // Aplicar una fuerza de salto
            rigidbody.linearVelocity = Vector3.up * jumpForce;
            audioSource.clip = jumpSound;
            audioSource.Play();
        } else
        {
            if (animator != null) animator.SetBool("IsFlapping", false);
        }

        // Control de aceleraci�n y desaceleraci�n en el eje X
        if (joystickAxis.x != 0 && !IsGamePaused())
        {
            if (animator != null) animator.SetBool("IsWalking", true);
            
            // Aumentar la velocidad en la direcci�n del joystick
            currentVelocity.x += joystickAxis.x * acceleration * Time.fixedDeltaTime;
            currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxSpeed, maxSpeed);

            if ((joystickAxis.x > 0 && transform.localScale.x < 0) || (joystickAxis.x < 0 && transform.localScale.x > 0))
            {
                // Cambiar la escala en X para voltear el personaje
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
        } else {
            if (animator != null) animator.SetBool("IsWalking", false);
            // Desacelerar cuando no se est� moviendo el joystick
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }

        // Aplicar la velocidad al Rigidbody
        rigidbody.linearVelocity = new Vector3(currentVelocity.x, rigidbody.linearVelocity.y, 0f);
        animator.SetFloat("yVelocity", rigidbody.linearVelocityY);
    }

    private bool IsGamePaused()
    {
        return PauseGame.isPaused;
    }

    public void Jump()
    {
        if (energyController.HasEnergyToFly() && !IsGamePaused())
        {
            // Aplicar una fuerza de salto
            rigidbody.linearVelocity = Vector3.up * jumpForce;
            audioSource.clip = jumpSound;
            audioSource.Play();

            OnJump?.Invoke();
        }

    }


    public void AllowMovement()
    {         // Permitir que el jugador se mueva
        enabled = true;
    }

    public void StopMovement()
    {
        // Detener el movimiento del jugador
        enabled = false;
        currentVelocity = Vector3.zero;
        rigidbody.linearVelocity = Vector3.zero;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public void SetJumpForce(float newJumpForce)
    {
        jumpForce = newJumpForce;
    }

}
