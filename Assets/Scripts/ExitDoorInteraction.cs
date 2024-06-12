using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorInteraction : MonoBehaviour
{
    public float openAngle = -80f;
    public float openSpeed = 2f;
    public float interactionDistance = 2f;
    public Animator textAnimator; // Reference to the Animator component for the text
    public string showTextTrigger = "OpenDoor"; // Trigger to show the prompt text
    public string hideTextTrigger = "HideText"; // Trigger to hide the prompt text
    public string needKeyTrigger = "NeedKey"; // Trigger to show the 'Need Key' animation
    public Animator animator;
    public AudioClip openSound; // Sound for opening the door
    public AudioClip closeSound; // Sound for closing the door
    public GameObject keyObject; // Reference to the key object

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
                if (GameManager.Instance.hasKey)
                {
                    textAnimator.SetTrigger(showTextTrigger); // Show the prompt animation
                }
                else
                {
                    textAnimator.SetTrigger(needKeyTrigger); // Show the 'Need Key' animation
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameManager.Instance.hasKey)
                {
                    isOpen = !isOpen;
                    PlayDoorSound();
                    animator.SetTrigger("Escaped");

                    if (isOpen)
                    {
                        UseKey();
                    }
                }
                else
                {
                    // Optionally, play a sound or show a message indicating that the door is locked
                }
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

    private void UseKey()
    {
        // Destroy the key object
        if (keyObject != null)
        {
            Destroy(keyObject);
        }

        // Set hasKey to false in the GameManager
        GameManager.Instance.hasKey = false;

        // Reload the scene after a short delay
        Invoke("ReloadScene", 2f); // Adjust delay if needed
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
