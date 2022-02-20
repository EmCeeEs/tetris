using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameManager GM;
    private PlayerControls PC;

    private Joystick joystick;
    private int cooldownTimer = 0;
    private const int MAX_COOLDOWN = 10;

    private void Start()
    {
        GM = GameManager.Instance;
        PC = new PlayerControls();

        joystick = GM.UI.GetComponent<UIHandler>().Joystick;

        PC.PlayerMovement.PlayerRotationLeft.started += RotateBoardLeft;
        PC.PlayerMovement.PlayerRotationRight.started += RotateBoardRight;
        PC.PlayerMovement.PlayerInvertBlockX.started += InvertBlockX;

        PC.Enable();
    }

    private void RotateBoardRight(InputAction.CallbackContext context)
    {
        GM.Store.Dispatch(new RotateRightAction());
    }

    public void RotateBoardLeft(InputAction.CallbackContext context)
    {
        GM.Store.Dispatch(new RotateLeftAction());
    }

    public void InvertBlockX(InputAction.CallbackContext context)
    {
        // GM.Store.Dispatch(new InvertXAction());
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer--;
        }
        else
        {
            if (joystick.Horizontal > 0.5)
            {
                GM.Store.Dispatch(new RotateRightAction());
                cooldownTimer = MAX_COOLDOWN;
            }

            if (joystick.Horizontal < -0.5)
            {
                GM.Store.Dispatch(new RotateLeftAction());
                cooldownTimer = MAX_COOLDOWN;
            }
        }
    }
}
