using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float mouseSensitivity = 2f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private float pitch = 0f;
    private float yaw = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
