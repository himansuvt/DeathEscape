using UnityEngine;

public class KeyInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Distance at which the player can interact with the key
    public GameObject keyHolder; // An empty GameObject in front of the camera to hold the key

    private bool isPlayerNearby = false;

    private void Update()
    {
        if (IsPlayerNearby())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GrabKey();
            }
        }
    }
    
    private bool IsPlayerNearby()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance <= interactionDistance;
    }

    private void GrabKey()
    {
        transform.SetParent(keyHolder.transform); // Make the key a child of the keyHolder
        transform.localPosition = Vector3.zero; // Move the key to the keyHolder's position
        transform.localRotation = Quaternion.identity; // Reset the key's rotation

        // Optionally, disable the key's collider if it has one
        Collider keyCollider = GetComponent<Collider>();
        if (keyCollider != null)
        {
            keyCollider.enabled = false;
        }
    }
}
