using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarSphereController : MonoBehaviour
{

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    // Inside CarSphereController class

    [SerializeField] private float wheelRotationSpeed = 360f; // Adjust the speed at which the wheels rotate
    [SerializeField] private float maxWheelTurnAngle = 30f; // Maximum angle the wheels can turn


    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;
    public Rigidbody sphereRB;
    public float revSpeed;
    public float turnSpeed;

    public float airDrag;
    public float groundDrag;

    public LayerMask groundLayer;

    public float fwdSpeed;

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    private Vector2 moveVector = Vector2.zero;

    private bool controlsEnabled = true;
    private void Awake()
    {
        inputAsset =  GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        move = player.FindAction("Move");
        move.performed += OnMovementPerformed;
        move.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        player.Disable();
        move.performed -= OnMovementPerformed;
        move.canceled -= OnMovementCanceled;
    }

    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        moveInput = moveVector.y;
        turnInput = moveVector.x;


        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;

        transform.position = sphereRB.transform.position;   

        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");

        transform.Rotate(0, newRotation, 0, Space.World);

        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 2f, groundLayer);

        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    
        if (isCarGrounded)
        {
            sphereRB.drag = groundDrag;

        }
        else
        {
            sphereRB.drag = airDrag;
        }

 


    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);

        }
        else
        {
            sphereRB.AddForce(transform.up * -20f);
        }

        float rotationAmount = moveInput * wheelRotationSpeed * Time.fixedDeltaTime;
        RotateWheels(rotationAmount);
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void UpdateSingleWheel(Transform wheelTransform)
    {
        Quaternion wheelRotation = Quaternion.Euler(sphereRB.rotation.eulerAngles.x, wheelTransform.rotation.eulerAngles.y, wheelTransform.rotation.eulerAngles.z);
        wheelTransform.rotation = wheelRotation;
    }

    // Method to rotate wheels based on car's movement
    private void RotateWheels(float rotationAmount)
    {
        float wheelTurnAngle = turnInput * maxWheelTurnAngle;

        // Rotate front wheels based on steering input
        RotateWheel(frontLeftWheelTransform, rotationAmount, wheelTurnAngle);
        RotateWheel(frontRightWheelTransform, rotationAmount, wheelTurnAngle);
        SpinWheel(rearLeftWheelTransform, rotationAmount);
        SpinWheel(rearRightWheelTransform, rotationAmount);

        // ... (rotate other wheels if necessary)
    }

    private void RotateWheel(Transform wheelTransform, float rotationAmount, float wheelTurnAngle)
    {
        wheelTransform.Rotate(rotationAmount, 0, 0, Space.Self);

        // Apply steering angle to the front wheels
        Vector3 localRotation = wheelTransform.localRotation.eulerAngles;
        localRotation.y = Mathf.Clamp(wheelTurnAngle, -maxWheelTurnAngle, maxWheelTurnAngle);
        wheelTransform.localRotation = Quaternion.Euler(localRotation);
    }

    private void SpinWheel(Transform wheelTransform, float rotationAmount)
    {
        wheelTransform.Rotate(rotationAmount, 0, 0, Space.Self);
    }


    private void EnableMovement()
    {
        if (controlsEnabled)
        {
            player.Enable();
        }
    }

    private void DisableMovement()
    {
        controlsEnabled = false;
        player.Disable();
    }

    // Method to disable controls
    public void DisableControls()
    {
        controlsEnabled = false;
        player.Disable(); // Disable the input actions
        // You might also want to reset the moveVector or perform any other necessary logic here
    }

}
