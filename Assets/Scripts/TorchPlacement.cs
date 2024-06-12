using UnityEngine;

public class TorchPlacement : MonoBehaviour
{
    public GameObject torchPrefab; // Reference to the torch prefab
    public Transform torchHolder; // Reference to the empty GameObject in front of the camera
    public KeyCode placementKey = KeyCode.T; // Key to place the torch

    [HideInInspector]
    public GameObject currentTorchInstance;
    [HideInInspector]
    public LineRenderer torchLineRenderer;

    private bool isTorchActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(placementKey))
        {
            // Toggle torch on key press
            ToggleTorch();
        }
    }

    private void ToggleTorch()
    {
        if (isTorchActive)
        {
            // If torch is active, deactivate it
            DeactivateTorch();
        }
        else
        {
            // If torch is inactive, activate it
            ActivateTorch();
        }
    }

    private void ActivateTorch()
    {
        // If torch instance already exists, return
        if (currentTorchInstance != null) return;

        // Instantiate the torch prefab at the torch holder's position and rotation
        currentTorchInstance = Instantiate(torchPrefab, torchHolder.position, torchHolder.rotation);

        // Rotate the torch by 90 degrees on the X-axis
        currentTorchInstance.transform.Rotate(Vector3.right, 90f);

        // Parent the torch to the torch holder to make it move with the player's view
        currentTorchInstance.transform.SetParent(torchHolder);

        // Get the LineRenderer component from the torch
        torchLineRenderer = currentTorchInstance.GetComponent<LineRenderer>();

        isTorchActive = true;
    }

    private void DeactivateTorch()
    {
        // If torch instance doesn't exist, return
        if (currentTorchInstance == null) return;

        // Destroy the torch instance
        Destroy(currentTorchInstance);

        torchLineRenderer = null;
        isTorchActive = false;
    }
}
