using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float mouseSensitivity = 2f;
    public AudioClip walkingSound; // Reference to the walking sound clip

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private AudioSource audioSource;
    private float pitch = 0f;
    private float yaw = 0f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Assign the walking sound clip to the AudioSource
        audioSource.clip = walkingSound;
        audioSource.loop = true; // Loop the walking sound
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 move = transform.forward * moveVertical + transform.right * moveHorizontal;
            moveDirection = move * moveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            // Play or stop the walking sound based on player movement
            if (move != Vector3.zero && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else if (move == Vector3.zero && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        float rotateHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
        float rotateVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += rotateHorizontal;
        pitch -= rotateVertical;

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.localEulerAngles = new Vector3(pitch, yaw, 0);
    }
}
