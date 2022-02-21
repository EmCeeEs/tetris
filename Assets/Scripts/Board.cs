using System;
using UnityEngine;

using PolarCoordinates;
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
                grid.MoveToSlot(new GridPoint(i, j), ref block);
                slotHolder[i, j] = block;
                slotHolder[i, j].SetActive(false);
            }
    }

    private void Start()
    {
        GM = GameManager.Instance;
        GM.Store.Subscribe(SyncState);
    }

    private void SyncState()
    {
        BoardState boardsState = GM.Store.GetState().Board;

        Action<GameObject, bool> SetActive = (go, flag) => go.SetActive(flag);

        // sync slots
        for (int i = 0; i < 12; i++)
            for (int j = 0; j < 12; j++)
                SetActive(slotHolder[i, j], boardsState.Slots[i, j]);

        // sync rotation
        playerBase.transform.rotation = Quaternion.Euler(
            0,
            grid.RotationAngle(boardsState.RotationOffset),
            0
        );
    }
}
