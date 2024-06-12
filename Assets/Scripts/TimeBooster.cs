using UnityEngine;

public class TimeBooster : MonoBehaviour
{
    public float timeBoostAmount = 30f; // Amount of time to boost the timer by
    public AudioClip boosterSound; // Sound effect for the booster

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Check if AudioSource is attached
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from this GameObject. Please add an AudioSource component.");
        }

        // Check if boosterSound is assigned
        if (boosterSound == null)
        {
            Debug.LogError("Booster sound is not assigned in the inspector. Please assign a sound clip.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.IncreaseTimer(timeBoostAmount); // Increase timer by the specified amount
            }
            else
            {
                Debug.LogError("TimerManager instance not found.");
            }

            if (audioSource != null && boosterSound != null)
            {
                audioSource.PlayOneShot(boosterSound); // Play the booster sound
            }

            // Destroy the TimeBooster object after the sound has finished playing
            Destroy(gameObject, boosterSound != null ? boosterSound.length : 0);
        }
    }
}
