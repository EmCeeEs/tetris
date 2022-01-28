using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float vertical;
    PlayerControls inputActions;

    public Transform playerBase;
    GameManager gameManager;

    public bool moveLeft;
    public bool moveRight;

    private int cooldownTimer = 0;
    private int MAX_COOLDOWN = 3;

    public Transform rayCastOrigin;
    public float scanRadius = 30f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        if (cooldownTimer == 0)
        {
            HandleRotation();
        }
        else
        {
            cooldownTimer -= 1;
        }
        //SlotStatusHandler();
    }

    void HandleRotation()
    {
        moveLeft = inputActions.PlayerMovement.PlayerRotationLeft.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
        moveRight = inputActions.PlayerMovement.PlayerRotationRight.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

        if (moveRight && !moveLeft)
        {
            playerBase.Rotate(new Vector3(0, 0, 1), 30);
            gameManager.state.rotateRight();
            cooldownTimer = MAX_COOLDOWN;
        }
        if (!moveRight && moveLeft)
        {
            playerBase.Rotate(new Vector3(0, 0, 1), -30);
            gameManager.state.rotateLeft();
            cooldownTimer = MAX_COOLDOWN;
        }


    }
}
