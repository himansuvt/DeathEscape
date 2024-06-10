using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = -80f;
    public float openSpeed = 2f;
    public float interactionDistance = 2f;
    public Animator textAnimator; // Reference to the Animator component for the text
     // The trigger name for showing the text animation

    private bool isPlayerNearby = false;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openAngle, transform.eulerAngles.z);
    }

    private void Update()
    {
        if (IsPlayerNearby())
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                textAnimator.SetTrigger("OpenDoor"); // Show the text animation
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen; // Toggle door state
            }
        }
        else
        {
            
                isPlayerNearby = false;
                textAnimator.SetTrigger("HideText"); // Hide the text animation if needed
            
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
}
