using UnityEngine;

public class KeyInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Distance at which the player can interact with the key
    public GameObject keyHolder; // An empty GameObject in front of the camera to hold the key
    public Animator animator;
    private bool isPlayerNearby = false;
    private bool hasKeyBeenPicked = false; // Track if the key has been picked up

    private void Update()
    {
        if (IsPlayerNearby())
        {
            if (!hasKeyBeenPicked)
            {
                animator.SetBool("Pick", true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GrabKey();
                }
            }
        }
        else
        {
            animator.SetBool("Pick", false);
        }
    }

    private bool IsPlayerNearby()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance <= interactionDistance;
    }

    private void GrabKey()
    {
        hasKeyBeenPicked = true;
        transform.SetParent(keyHolder.transform); // Make the key a child of the keyHolder
        transform.localPosition = Vector3.zero; // Move the key to the keyHolder's position
        transform.localRotation = Quaternion.Euler(0, 0, 90); // Reset the key's rotation

        Collider keyCollider = GetComponent<Collider>();
        if (keyCollider != null)
        {
            keyCollider.enabled = false;
        }

        GameManager.Instance.GrabKey(); // Notify the GameManager that the key has been grabbed

        // Optionally, reset the animation state after a delay
        Invoke("ResetAnimation", 1f); // Adjust the delay based on your animation length
    }

    private void ResetAnimation()
    {
        animator.SetBool("Pick", false);
    }
}
