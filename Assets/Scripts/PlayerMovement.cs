using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Editable Values")]
    [SerializeField] public float movementSpeed = 12f;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public float groundDistance = 0.4f;
    [SerializeField] public float sprintSpeed = 7f;
    [SerializeField] public int playerStamina = 100;
    //[SerializeField] private float _timeBetweenRegen = 0.2f;
    [SerializeField] private bool useFootSteps = true;
    [SerializeField] private bool useHeadBob = true;

    [Header("Connections and Selections")]
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public CharacterController charController;
    [SerializeField] public Camera camComponent;
    [SerializeField] public Slider staminaBar;
    [SerializeField] public AudioClip _footstepClip;

    [Header("Footstep Variables")]
    [SerializeField] private float _baseStepSpeed = 0.5f;
    [SerializeField] private float _sprintMultiplier = 0.6f;
    [SerializeField] private AudioSource _footstepSource = default;
    [SerializeField] private List<AudioClip> _footstepClipsList = new List<AudioClip>();
    private float footStepTimer = 0f;
    private float GetCurrentOffset => isSprinting ? _baseStepSpeed * _sprintMultiplier : _baseStepSpeed;

    [Header("Headbob Variables")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.5f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 1f;
    private float defaultCameraYPos = 0;
    private float headBobTimer;



    private float currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    private Coroutine regen;

    private Vector3 move;

    bool isSprinting = false;
    bool isWalking = true;

    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        defaultCameraYPos = camComponent.transform.localPosition.y;
    }


    void Start()
    {
        currentStamina = playerStamina;
        staminaBar.maxValue = playerStamina;
        staminaBar.value = playerStamina;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerStamina);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (currentStamina > 1)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = true;
                isWalking = false;
                UseStamina(0.4f);
                sprintSpeed = 10f;
                camComponent.fieldOfView = Mathf.Lerp(60f, 70f, 10f);

            }
            else
            {
                isWalking = true;
                isSprinting = false;
                //sprintSpeed = 0f;
                camComponent.fieldOfView = Mathf.Lerp(70f, 60f, 10f);
            }

        }
        else if (currentStamina <= 1)
        {
            isWalking = true;
            isSprinting = false;
            sprintSpeed = 0f;
            camComponent.fieldOfView = Mathf.Lerp(70f, 60f, 10f);
        }

        if (useFootSteps)
        {
            HandleFootsteps();
        }

        if (useHeadBob)
        {
            HandleHeadBob();
        }

        move = transform.right * moveX + transform.forward * moveZ;

        // charController.Move(move * (movementSpeed + sprintSpeed) * Time.deltaTime);
        charController.Move(move * (movementSpeed + sprintSpeed) * Time.deltaTime);

        //if (_footstepClip != null)
        //{
        //    AudioHelper.PlayClip2D(_footstepClip, 1f);
        //}

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        charController.Move(velocity * Time.deltaTime);

        if (isSprinting && currentStamina > 0)
        {
            camComponent.fieldOfView = Mathf.Lerp(60f, 70f, 10f);
            sprintSpeed = 7f;
            UseStamina(0.10f);

            if (currentStamina == 0)
            {
                isSprinting = false;
            }

        }
        if (isWalking)
        {
            sprintSpeed = 0f;
            camComponent.fieldOfView = Mathf.Lerp(70f, 60f, 10f);
        }
    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
    }

    private void HandleFootsteps()
    {
        if (!isGrounded) return;

        if (move == Vector3.zero) return;

        footStepTimer -= Time.deltaTime;

        if (footStepTimer <= 0)
        {
            _footstepSource.PlayOneShot(_footstepClipsList[Random.Range(0, _footstepClipsList.Count - 1)]);
            footStepTimer = GetCurrentOffset;
        }
    }

    private void HandleHeadBob()
    {
        if (!isGrounded) return;

        if (move == Vector3.zero) return;

        if (move != Vector3.zero)
        {
            headBobTimer += Time.deltaTime * (isWalking ? walkBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);
            camComponent.transform.localPosition = new Vector3(
                camComponent.transform.localPosition.x,
                defaultCameraYPos + Mathf.Sin(headBobTimer) * (isWalking ? walkBobAmount : isSprinting ? sprintBobAmount: walkBobAmount),
                camComponent.transform.localPosition.z);
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);
        while (currentStamina < playerStamina)
        {
            currentStamina += playerStamina / 150;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
    }

}
