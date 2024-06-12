using UnityEngine;

public class GhostController : MonoBehaviour
{
    private GhostAI ghostAI;
    public AudioClip normalBreathClip;
    public AudioClip heavyBreathClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Get reference to the GhostAI component
        ghostAI = FindObjectOfType<GhostAI>();
    }

    void Update()
    {
        // Check if the GhostAI component is still valid
        if (ghostAI != null && ghostAI.isActiveAndEnabled)
        {
            if (ghostAI.isPlayerInRange)
            {
                // If the player is in range, play the heavy breathing clip
                if (audioSource.clip != heavyBreathClip)
                {
                    audioSource.clip = heavyBreathClip;
                    audioSource.Play();
                }
            }
            else
            {
                // If the player is not in range, play the normal breathing clip
                if (audioSource.clip != normalBreathClip)
                {
                    audioSource.clip = normalBreathClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            // If the GhostAI component is not valid or not active, stop playing any breathing sound
            audioSource.Stop();
        }
    }
}
