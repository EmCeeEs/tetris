using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager GM;

    private void Awake()
    {
        GM = GameManager.Instance;
    }

    public void RotateBoard()
    {
        State state = GM.Store.GetState();

        // TODO: check validity

        var action = ActionCreators.CreateRotateAction(1);
        GM.Store.Dispatch(action);
    }

    public void InvertBlock()
    {
        State state = GM.Store.GetState();

        // TODO: check validity

        var action = ActionCreators.CreateInvertAction();
        GM.Store.Dispatch(action);
    }
}
