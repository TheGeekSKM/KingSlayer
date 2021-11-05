using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Connections")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;


   

    [Header("Player Attributes")]
    [SerializeField] public float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public float additionalSprintSpeed;

    float turnSmoothVelocity;
    Vector3 currentVelocity;
    bool isGrounded;
    bool isSprinting;


   

    void Update()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(0, 45f, 0);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && currentVelocity.y < 0)
        {
            currentVelocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

       

       if (isSprinting)
        {
            controller.Move(transform.forward * (speed + additionalSprintSpeed) * Time.deltaTime);
        }
        else
        {
            controller.Move(transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            currentVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        currentVelocity.y += gravity * Time.deltaTime;
        controller.Move(currentVelocity * Time.deltaTime);
    }
}
