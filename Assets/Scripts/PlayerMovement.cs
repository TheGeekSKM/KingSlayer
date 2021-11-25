using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] public float movementSpeed = 12f;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public float groundDistance = 0.4f;
    [SerializeField] public float sprintSpeed = 7f;
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public CharacterController charController;
    [SerializeField] public Camera camComponent;
    [SerializeField] public int playerStamina = 100;
    // [SerializeField] public Slider staminaBar;


    private float currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    

    bool isSprinting = false;
    bool isWalking = true;

    Vector3 velocity;
    bool isGrounded;
    

    void Start()
    {
        currentStamina = playerStamina;
        // staminaBar.maxValue = playerStamina;
        // staminaBar.value = playerStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerStamina);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // if (currentStamina > 1)
        // {
        //     if (Input.GetKey(KeyCode.LeftShift))
        //     {
        //         isSprinting = true;
        //         isWalking = false;
        //         UseStamina(0.4f);
        //         sprintSpeed = 10f;
        //         camComponent.fieldOfView = Mathf.Lerp(60f, 70f, 10f);

        //     }

        // }
        // else if (currentStamina <= 1)
        // {
        //     isWalking = true;
        //     isSprinting = false;
        //     sprintSpeed = 0f;
        //     camComponent.fieldOfView = Mathf.Lerp(70f, 60f, 10f);
        // }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // charController.Move(move * (movementSpeed + sprintSpeed) * Time.deltaTime);
        charController.Move(move * (movementSpeed + sprintSpeed) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        charController.Move(velocity * Time.deltaTime);

        //if (isSprinting && currentStamina > 0)
        //{
        //    camComponent.fieldOfView = Mathf.Lerp(60f, 70f, 10f);
        //    sprintSpeed = 7f;
        //    UseStamina(0.40f);

        //    if (currentStamina == 0)
        //    {
        //        isSprinting = false;
        //    }
            
        //}
        //if (isWalking)
        //{
        //    sprintSpeed = 0f;
        //    camComponent.fieldOfView = Mathf.Lerp(70f, 60f, 10f);
        //}
    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            // staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);
        while (currentStamina < playerStamina)
        {
            currentStamina += playerStamina / 100;
            // staminaBar.value = currentStamina;
            yield return regenTick;
        }
    }

}
