using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarSphereController : MonoBehaviour
{
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


        //sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
}
