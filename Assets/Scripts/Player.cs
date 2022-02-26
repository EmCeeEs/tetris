using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private GameManager GM;

	private PlayerControls inputActions;

	private int cooldownTimer = 0;
	private const int MAX_COOLDOWN = 5;

	private void Awake()
	{
		GM = GameManager.Instance;
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
			HandleXInversion();
		}
		else
		{
			cooldownTimer -= 1;
		}
	}

	private void HandleXInversion()
	{
		bool invertion = inputActions.PlayerMovement.PlayerInvertBlockX.phase == InputActionPhase.Performed;

		if (GM.UIHandler.Joystick.Vertical > 0.5 || GM.UIHandler.Joystick.Vertical < -0.5)
		{
			invertion = true;
		}

		if (GM.currentBlock)
		{
			BlockParent block = GM.currentBlock.GetComponent<BlockParent>();
			List<Slot> newLayout = LayoutCreator.InvertX(block.state.BlockLayout);

			bool canInvert = newLayout.All(slot => GM.Board.IsEmpty(slot + GridUtils.SnapToNextX(block.state.Position)));

			if (canInvert && invertion)
			{
				block.InvertX();

				cooldownTimer = MAX_COOLDOWN;
			}
		}
	}

	private void HandleRotation()
	{
		bool moveLeft = inputActions.PlayerMovement.PlayerRotationLeft.phase == InputActionPhase.Performed;
		bool moveRight = inputActions.PlayerMovement.PlayerRotationRight.phase == InputActionPhase.Performed;

		if (GM.UIHandler.Joystick.Horizontal > 0.5)
		{
			moveLeft = true;
		}
		if (GM.UIHandler.Joystick.Horizontal < -0.5)
		{
			moveRight = true;
		}

		if (moveRight)
		{
			GM.Board.RotateRight();

			cooldownTimer = MAX_COOLDOWN;
		}
		if (moveLeft)
		{
			GM.Board.RotateLeft();
			cooldownTimer = MAX_COOLDOWN;
		}
	}
}
