using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using System.Linq;
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

        inputActions.PlayerMovement.PlayerInvertBlockX.started += HandleXInversion;
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

    private void HandleXInversion(InputAction.CallbackContext context)
    {
        BlockParent block = board.currentBlock.GetComponent<BlockParent>();
        List<Slot> newLayout = LayoutCreator.InvertX(block.BlockLayout);

        bool canInvert = newLayout.All(slot => board.IsEmpty(slot + block.LowerSlot));

        if (canInvert)
        {
            block.InvertX();
        }
    }

    private void HandleRotation()
    {
        bool moveLeft = inputActions.PlayerMovement.PlayerRotationLeft.phase == InputActionPhase.Performed;
        bool moveRight = inputActions.PlayerMovement.PlayerRotationRight.phase == InputActionPhase.Performed;

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
