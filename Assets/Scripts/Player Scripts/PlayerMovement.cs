using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private float turnDeadzone = 0.3f;
    [SerializeField]
    private int playerNumber = 1;


    private Rigidbody playerRigidbody;

    private string horizontalInputName;         // The name of the input axis for moving left and right.
    private string verticalInputName;           // The name of the input axis for moving forward and back.
    private string turnHorizontalInputName;     // The name of the input axis for roatating left and right.
    private string turnVerticalInputName;       // The name of the input axis for roatating forward and back.

    private float horizontalInput;              // The current value of the horizontal movement input.
    private float verticalInput;                // The current value of the vertical movement input.

    // Setting current value of the horizontal turn input and stopping the rotation snap back.
    private float TurnHorizontalInput
    {
        get
        {
            float input = Input.GetAxisRaw(turnHorizontalInputName);
            if (Mathf.Abs(input) < turnDeadzone)
            {
                input = 0;
            }
            return input;
        }
    }

    // Setting current value of the horizontal turn input and stopping the rotation snap back.
    private float TurnVerticalInput
    {
        get
        {
            float input = Input.GetAxisRaw(turnVerticalInputName);
            if (Mathf.Abs(input) < turnDeadzone)
            {
                input = 0;
            }
            return input;
        }
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // When the player is turned on, make sure it's not kinematic.
        playerRigidbody.isKinematic = false;

        // Also reset the input values.
        horizontalInput = 0f;
        verticalInput = 0f;
    }

    private void OnDisable()
    {
        // When the player is turned off, set it to kinematic so it stops moving.
        playerRigidbody.isKinematic = true;
    }

    private void Start()
    {
        // The input names are based on player number.
        horizontalInputName = "Horizontal" + playerNumber;
        verticalInputName = "Vertical" + playerNumber;
        turnHorizontalInputName = "TurnHorizontal" + playerNumber;
        turnVerticalInputName = "TurnVertical" + playerNumber;
    }

    private void Update()
    {
        // Store the value of the movement inputs.
        horizontalInput = Input.GetAxis(horizontalInputName);
        verticalInput = Input.GetAxis(verticalInputName);

    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }

    private void Turn()
    {

        if (Mathf.Abs(TurnVerticalInput) > 0 || Mathf.Abs(TurnHorizontalInput) > 0)
        {
            Vector3 turnDirection = new Vector3(TurnHorizontalInput, 0, TurnVerticalInput);

            Quaternion newRotation = Quaternion.LookRotation(turnDirection);
            newRotation.eulerAngles = new Vector3(0, newRotation.eulerAngles.y, 0);
            playerRigidbody.MoveRotation(newRotation);
        }
    }
}
