using UnityEngine;

public class Board : MonoBehaviour
{
    private GameManager GM;

    private GameObject playerBase;

    private void Awake()
    {
        playerBase = GameObject.FindWithTag("Base");
    }

    private void Start()
    {
        GM = GameManager.Instance;
        GM.Store.Subscribe(SyncState);
    }

    private void SyncState(State state)
    {
        int nCols = 12;
        int nRotation = Utils.Mod(state.Board.RotationOffset, nCols);
        float rotationAngle = 360 / nCols;
        playerBase.transform.rotation = Quaternion.Euler(0, nRotation * rotationAngle, 0);
    }
}
