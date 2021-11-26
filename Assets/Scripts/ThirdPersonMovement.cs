using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    
    [Header("Player Attributes")]
    [SerializeField] public float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public float additionalSprintSpeed;


    Vector3 currentVelocity;
    bool isGrounded;
    bool isSprinting;


   

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
    }
}
