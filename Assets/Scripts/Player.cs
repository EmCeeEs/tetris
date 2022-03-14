using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private GameManager GM;

	private PlayerControls inputActions;

	private int cooldownTimer = 0;

	public bool moveLeft;
	public bool moveRight;
	public bool isInversion;
	public bool dropDown;

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
		HandleInput();	
	}

	private void LateUpdate()
	{
		HandleRotation();
		HandleXInversion();
		HandleDropDown();

		ClearBooleans();
	}

	private void HandleInput()
	{
		dropDown = inputActions.PlayerMovement.PlayerDropdownBlock.phase == InputActionPhase.Performed;

		isInversion |= inputActions.PlayerMovement.PlayerInvertBlockX.phase == InputActionPhase.Performed;
		isInversion |= GM.UIHandler.Joystick.Vertical > 0.5 || GM.UIHandler.Joystick.Vertical < -0.5;

		moveLeft = inputActions.PlayerMovement.PlayerRotationLeft.phase == InputActionPhase.Performed;
		moveRight = inputActions.PlayerMovement.PlayerRotationRight.phase == InputActionPhase.Performed;

		if (GM.UIHandler.Joystick.Horizontal > 0.5)
		{
			moveLeft = true;
		}
		if (GM.UIHandler.Joystick.Horizontal < -0.5)
		{
			moveRight = true;
		}
	}

	private void HandleDropDown()
	{
		if (GM.currentBlock == null)
			return;

		if (dropDown) { Debug.Log("Drop Down"); }
		
	}
	private void HandleXInversion()
	{
		if (GM.currentBlock == null)
			return;

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

	public void HandleRotationLeft()
	{
		moveLeft = true;
	}
	public void HandleRotationRight()
	{
		moveRight = true;
	}
	public void HandleInversion()
	{
		isInversion = true;
	}
	private void ClearBooleans()
	{
		moveLeft = false;
		moveRight = false;
		isInversion = false;
	}
}
