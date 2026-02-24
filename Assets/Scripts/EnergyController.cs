using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class EnergyController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private Slider slider;
    [SerializeField] private float energyPerJump = 2.5f;
    [SerializeField] private int energyRecoveryPerDelay = 4;
    [SerializeField] private float recoveryDelay = 0.25f; // Nuevo campo para ajustar el retraso
    private float currentEnergy;
    private bool isRecovering = false;

    private void OnEnable()
    {
        // Subscribe to the jump event
        PlayerController.OnJump += OnPlayerJump;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        PlayerController.OnJump -= OnPlayerJump;
    }

    private void Update()
    {
        if (ShouldRecoverEnergy())
        {
            StartCoroutine(RecoverEnergy());
        }
    }

    private void Start()
    {
        currentEnergy = maxEnergy;
        if (slider == null)
        {
            Debug.LogError("Slider reference is missing in EnergyController");
        }
    }

    public void OnPlayerJump()
    {
        currentEnergy -= energyPerJump;
        slider.value = currentEnergy;
    }

    public bool HasEnergyToFly() => currentEnergy > 0;

    private bool ShouldRecoverEnergy() => playerController != null && slider != null && playerController.IsGrounded() && currentEnergy < maxEnergy && !isRecovering;

    private IEnumerator RecoverEnergy()
    {
        isRecovering = true;
        yield return new WaitForSeconds(recoveryDelay);
        currentEnergy += energyRecoveryPerDelay;
        //slider.value = currentEnergy;
        isRecovering = false;
    }

    private void FixedUpdate()
    {
        //if (slider != null)
        slider.value = currentEnergy;
    }

    public float GetCurrentEnergy() => currentEnergy;

    public void setNewEnergyPerJump(float newEnergyJump)
    {
        energyPerJump = newEnergyJump;
    }

    public void SetCurrentEnergy(float newCurrent)
    {
        currentEnergy = newCurrent;
    }
}
