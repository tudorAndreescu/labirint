using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(TrailRenderer))]

public class FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    private GameObject batteryPickedUp;


    CharacterController characterController;
    TrailRenderer trailRenderer;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool isTrailing = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        trailRenderer = GetComponent<TrailRenderer>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run (doesn't work while moving backwards)
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isMovingBackwards = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        float curSpeedX = canMove ? (isRunning && !isMovingBackwards ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning && !isMovingBackwards ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


        // Trailing
        if (Input.GetMouseButtonDown(0)) isTrailing = !isTrailing;

        trailRenderer.emitting = isTrailing;
        if (!isTrailing) trailRenderer.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Secret"))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            StaticValues.gameWon = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.tag == "Battery")
        {
            batteryPickedUp = other.gameObject;
            Flashlight.currentEnergy += 60;
            Destroy(batteryPickedUp);
        }
    }
    
}