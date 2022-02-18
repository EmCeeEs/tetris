using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GameManager GM;

    private GameObject playerBase;
    private GameObject[,] slotHolder;
    private PolarGrid grid;

    public GameObject BlockPrefab;

    private void Awake()
    {
        playerBase = GameObject.FindWithTag("Base");
        slotHolder = new GameObject[12, 12];
        grid = new PolarGrid();

        GameObject block;
        for (int i = 0; i < 12; i++)
            for (int j = 0; j < 12; j++)
            {
                block = Instantiate(BlockPrefab, playerBase.transform);
                grid.MoveToSlot(new Slot(i, j), ref block);
                slotHolder[i, j] = block;
                slotHolder[i, j].SetActive(false);
            }
    }

    private void Start()
    {
        GM = GameManager.Instance;
        GM.Store.Subscribe(SyncState);
    }

    private void SyncState(State state)
    {
        Action<GameObject, bool> SetActive = (go, flag) => go.SetActive(flag);
        // Action<GameObject> Activate = (go) => SetActive(go, true);
        // Action<GameObject> Deactivate = (go) => SetActive(go, false);

        // sync slots
        for (int i = 0; i < 12; i++)
            for (int j = 0; j < 12; j++)
                SetActive(slotHolder[i, j], state.Board.Slots[i, j]);

        // sync rotation
        playerBase.transform.rotation = Quaternion.Euler(
            0,
            grid.RotationAngle(state.Board.RotationOffset),
            0
        );
    }
}
