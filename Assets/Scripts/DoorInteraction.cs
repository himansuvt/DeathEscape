using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = -80f;
    public float openSpeed = 2f;
    public float interactionDistance = 2f;
    public Animator textAnimator; // Reference to the Animator component for the text
    public string showTextTrigger = "OpenDoor"; // Trigger to show the prompt text
    public string hideTextTrigger = "HideText"; // Trigger to hide the prompt text

    public AudioClip openSound; // Sound for opening the door
    public AudioClip closeSound; // Sound for closing the door

    private AudioSource audioSource; // AudioSource component to play the sounds
    private bool isPlayerNearby = false;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openAngle, transform.eulerAngles.z);

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (IsPlayerNearby())
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                textAnimator.SetTrigger(showTextTrigger); // Show the prompt animation
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;
                PlayDoorSound(); // Play the appropriate sound
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                textAnimator.SetTrigger(hideTextTrigger); // Hide the prompt animation if player moves away
            }
        }

        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }

    private bool IsPlayerNearby()
    {
        return Vector3.Distance(transform.position, Camera.main.transform.position) <= interactionDistance;
    }

    private void PlayDoorSound()
    {
        if (audioSource != null)
        {
            if (isOpen && openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
            else if (!isOpen && closeSound != null)
            {
                audioSource.PlayOneShot(closeSound);
            }
        }
    }
}
