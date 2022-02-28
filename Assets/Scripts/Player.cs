using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private GameManager GM;

	private PlayerControls inputActions;

	private int cooldownTimer = 0;

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
		if (cooldownTimer != 0)
		{
			cooldownTimer--;
			return;
		}

		HandleRotation();
		HandleXInversion();
	}

	private void HandleXInversion()
	{
		if (GM.currentBlock == null)
			return;

		bool isInversion = false;

		isInversion |= inputActions.PlayerMovement.PlayerInvertBlockX.phase == InputActionPhase.Performed;
		isInversion |= GM.UIHandler.Joystick.Vertical > 0.5 || GM.UIHandler.Joystick.Vertical < -0.5;

		if (isInversion)
		{
			BlockParent block = GM.currentBlock.GetComponent<BlockParent>();
			List<Slot> newLayout = LayoutCreator.InvertX(block.state.BlockLayout);

			Point positionMargin = new Point(GM.Settings.Speed.PositionMargin, 0);
			Slot nextSlot = GridUtils.SnapToNextX(block.state.Position + positionMargin);

			bool canInvert = newLayout.All(slot => GM.Board.IsEmpty(slot + nextSlot));

			if (canInvert)
			{
				block.InvertX();

				cooldownTimer = GM.Settings.Speed.PlayerCooldown;
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

			cooldownTimer = GM.Settings.Speed.PlayerCooldown;
		}
		if (moveLeft)
		{
			GM.Board.RotateLeft();
			cooldownTimer = GM.Settings.Speed.PlayerCooldown;
		}
	}
}
