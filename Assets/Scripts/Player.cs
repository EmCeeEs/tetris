using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameManager GM;
    private PlayerControls PC;

    private void Start()
    {
        GM = GameManager.Instance;
        PC = new PlayerControls();

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
        GM.Store.Dispatch(new InvertXAction());
    }
}
