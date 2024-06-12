using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public AudioClip jumpScareSound; // Jump scare sound effect

    private AudioSource audioSource; // AudioSource component to play the sounds

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            GhostAI[] ghosts = GetComponentsInChildren<GhostAI>();
            foreach (GhostAI ghost in ghosts)
            {
                ghost.SetPlayerInRange(true);
            }

            // Play jump scare sound effect when the player enters the room
            audioSource.PlayOneShot(jumpScareSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            GhostAI[] ghosts = GetComponentsInChildren<GhostAI>();
            foreach (GhostAI ghost in ghosts)
            {
                ghost.SetPlayerInRange(false);
            }
        }
    }
}
