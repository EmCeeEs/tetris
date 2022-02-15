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

    public void RotateBoardRight(InputAction.CallbackContext context)
    {
        var action = new RotateAction(1);
        GM.Store.Dispatch(action);
    }

    public void RotateBoardLeft(InputAction.CallbackContext context)
    {
        var action = new RotateAction(-1);
        GM.Store.Dispatch(action);
    }

    public void InvertBlockX(InputAction.CallbackContext context)
    {
        var action = new InvertAction();
        GM.Store.Dispatch(action);
    }
}
