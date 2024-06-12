using UnityEngine;

public class PostBoxInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Distance at which the player can interact with the post box
    public GameObject letter; // Reference to the letter GameObject inside the post box
    public Animator hintAnimator; // Animator for showing hint text
    public string showTextTrigger = "ShowText"; // Trigger to show the prompt text
    public string hideTextTrigger = "HideText"; // Trigger to hide the prompt text
    public string hintTrigger = "Hint1"; // Trigger for the hint animation
    public AudioClip pickupSound; // Sound effect for picking up the letter

    private bool isPlayerNearby = false;
    private bool hasLetter = true; // Indicates if the post box still has the letter
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void Update()
    {
        if (IsPlayerNearby())
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                hintAnimator.SetTrigger(showTextTrigger); // Show the prompt animation
            }

            if (hasLetter && Input.GetKeyDown(KeyCode.F))
            {
                DestroyLetter();
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                hintAnimator.SetTrigger(hideTextTrigger); // Hide the prompt animation if player moves away
            }
        }
    }

    private bool IsPlayerNearby()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance <= interactionDistance;
    }

    private void DestroyLetter()
    {
        audioSource.PlayOneShot(pickupSound); // Play the pickup sound
        Destroy(letter); // Destroy the letter GameObject
        hasLetter = false; // Update the state to indicate the letter is gone
        hintAnimator.SetTrigger(hintTrigger); // Show the hint animation
        hintAnimator.ResetTrigger(showTextTrigger); // Hide the prompt animation
        hintAnimator.SetTrigger(hideTextTrigger); // Hide the prompt text
    }
}
