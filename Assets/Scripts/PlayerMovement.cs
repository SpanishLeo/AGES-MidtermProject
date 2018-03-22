using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region serialize fields
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private int debugPlayerNumberOverride = 0;
    #endregion

    #region private fields
    private Rigidbody rigidbody;
    private Text playerNumberLabel;
    private PlayerManager controllingPlayer_UseProperty;
    #endregion

    #region public properties
    // Which player controls the character?
    // We will use the Player.PlayerNumber to
    // decide which GamePad to look at.
    public PlayerManager ControllingPlayer
    {
        get { return controllingPlayer_UseProperty; }
        set
        {
            controllingPlayer_UseProperty = value;
            UpdatePlayerIndexLabelText();
        }
    }
    #endregion

    #region private properties
    private float HorizontalInput
    {
        get { return Input.GetAxis(HorizontalInputName); }
    }

    private float VerticalInput
    {
        get { return Input.GetAxis(VerticalInputName); }
    }

    // You must configure the Unity Input Manager
    // to have an axis for each control for each supported player.
    // Begin numbering at 1, as Unity numbers "joysticks" beginning at 1.
    // "Joystick" means gamepad in real life...
    private string HorizontalInputName
    {
        get
        {
            return "Horizontal" + ControllingPlayer.PlayerNumber;
        }
    }

    private string VerticalInputName
    {
        get
        {
            return "Vertical" + ControllingPlayer.PlayerNumber;
        }
    }

    private string FireInputName
    {
        get
        {
            return "Fire" + ControllingPlayer.PlayerNumber;
        }
    }
    #endregion

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerNumberLabel = GetComponentInChildren<Text>();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.position * HorizontalInput * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = VerticalInput * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }

    private void UpdatePlayerIndexLabelText()
    {
        playerNumberLabel.text = ControllingPlayer.PlayerNumber.ToString();
    }


    void Start()
    {
#if UNITY_EDITOR
        if (debugPlayerNumberOverride > 0)
        {
            ControllingPlayer = new PlayerManager(debugPlayerNumberOverride);
        }
#endif
    }
}
