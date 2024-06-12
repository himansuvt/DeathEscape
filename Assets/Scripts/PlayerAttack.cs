using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera in the Inspector
    public TorchPlacement torchPlacement; // Assign the TorchPlacement component in the Inspector
    public AudioSource audioSource; // Assign the AudioSource component in the Inspector
    public AudioClip shootingSound; // Assign the shooting sound in the Inspector
    public float lineDuration = 0.1f; // Duration the line will be visible

    private void Start()
    {
        // Ensure AudioSource is properly set up
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (torchPlacement.currentTorchInstance != null && torchPlacement.torchLineRenderer != null)
            {
                LineRenderer lineRenderer = torchPlacement.torchLineRenderer;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // Play the shooting sound
                    audioSource.PlayOneShot(shootingSound);

                    // Set the positions of the line renderer
                    lineRenderer.SetPosition(0, torchPlacement.currentTorchInstance.transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                    StartCoroutine(ShowLine(lineRenderer));

                    if (hit.collider.CompareTag("Ghost"))
                    {
                        GhostAI ghost = hit.collider.GetComponent<GhostAI>();
                        if (ghost != null)
                        {
                            ghost.TakeDamage();
                        }
                    }
                }
            }
        }
    }

    private IEnumerator ShowLine(LineRenderer lineRenderer)
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }
}
