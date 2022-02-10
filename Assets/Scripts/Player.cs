using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls inputActions;
    public Joystick joystick;

    private Board board;

    private int cooldownTimer = 0;
    private const int MAX_COOLDOWN = 4;

    private void Awake()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
    }

    private void OnEnable()
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
    }

    private void HandleRotation()
    {
        bool moveLeft = inputActions.PlayerMovement.PlayerRotationLeft.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
        bool moveRight = inputActions.PlayerMovement.PlayerRotationRight.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

        if (joystick.Horizontal > 0.5)
        {
            moveLeft = true;
        }
        if (joystick.Horizontal < -0.5)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            board.RotateRight();

            cooldownTimer = MAX_COOLDOWN;
        }
        if (moveLeft)
        {
            board.RotateLeft();
            cooldownTimer = MAX_COOLDOWN;
        }
    }
}
