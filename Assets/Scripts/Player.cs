using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerControls inputActions;

    private Board board;

    private int cooldownTimer = 0;
    [SerializeField]
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
