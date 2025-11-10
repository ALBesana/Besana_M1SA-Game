using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    [Header("Movement Settings")]
    public Camera playerCamera;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float jumpPower = 15f;
    public float gravity = 20f;

    [Header("Jump Behavior")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public int maxJumps = 2;

    [Header("Camera & Crouch Settings")]
    public float lookSpeed = 2.5f;
    public float lookXLimit = 55f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    [Header("Water / Fall Check")]
    public float waterYLevel = -5f; // set this to your water plane Y position

    // --- Bounce ---
    private float bounceY = 0f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private int jumpCount = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        Vector3 horizontalMove = (forward * curSpeedX) + (right * curSpeedY);

        if (bounceY > 0f)
        {
            moveDirection.y = bounceY;
            bounceY = 0f;
        }

        if (characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && canMove && jumpCount < maxJumps)
        {
            moveDirection.y = jumpPower;
            jumpCount++;
        }

        // --- GRAVITY ---
        if (!characterController.isGrounded)
        {
            if (moveDirection.y < 0)
                moveDirection.y -= gravity * fallMultiplier * Time.deltaTime;
            else if (moveDirection.y > 0 && !Input.GetButton("Jump"))
                moveDirection.y -= gravity * lowJumpMultiplier * Time.deltaTime;
            else
                moveDirection.y -= gravity * Time.deltaTime;
        }

        // --- HORIZONTAL MOVEMENT ---
        moveDirection.x = horizontalMove.x;
        moveDirection.z = horizontalMove.z;

        if (Input.GetKey(KeyCode.C) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        if (transform.position.y < waterYLevel)
        {
            SceneManager.LoadScene("GameOver");
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        if (!canMove) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    public void ApplyBounce(float force)
    {
        if (force > bounceY)
            bounceY = force;
    }
}
