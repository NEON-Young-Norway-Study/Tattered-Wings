using UnityEngine;
using System.Collections;

public class AmbienceSoundManager : MonoBehaviour
{


    [SerializeField] private AudioClip[] ambienceSounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float interval = 10f; // Intervalo de tiempo en segundos

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // PlayerController.OnJump += StopAmbienceSound;
        DialogueOneStart.OnStopAmbienceSounds += StopAmbienceSound;
        StartCoroutine("PlayAmbienceSounds");
    }

    private IEnumerator PlayAmbienceSounds()
    {
        while (true)
        {
            PlayRandomAmbienceSound();
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento OnJump
        // PlayerController.OnJump -= StopAmbienceSound;
    }

    private void PlayRandomAmbienceSound()
    {
        if (ambienceSounds.Length == 0) return;

        int randomIndex = Random.Range(0, ambienceSounds.Length);
        audioSource.clip = ambienceSounds[randomIndex];
        audioSource.Play();
    }

    private void StopAmbienceSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        StopCoroutine("PlayAmbienceSounds");
    }
}
