using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GameManager GM;
    private GameObject playerBase;
    private GameObject[,] slots;

    private void Start()
    {
        GM = GameManager.Instance;
        GM.Store.Subscribe(SyncState);
    }

    private void SyncState()
    {
        State state = GM.Store.GetState();
        playerBase ??= Instantiate(GM.prefabManager.PlayerBasePrefab, transform);
        slots ??= CreateSlots(state);

        Action<GameObject, bool> SetActive = (go, flag) => go.SetActive(flag);
        int xDim = state.Grid.GetLength(0);
        int yDim = state.Grid.GetLength(1);

        for (int i = 0; i < xDim; i++)
            for (int j = 0; j < yDim; j++)
                SetActive(slots[i, j], state.Grid[i, j]);

        // sync rotation
        playerBase.transform.rotation = Quaternion.Euler(
            0,
            Utils.Mod(state.RotationOffset, yDim) * 360 / yDim,
            0
        );
    }

    public GameObject[,] CreateSlots(State state)
    {
        int xDim = state.Grid.GetLength(0);
        int yDim = state.Grid.GetLength(1);

        GameObject[,] slots = new GameObject[xDim, yDim];

        GameObject slotHolder = new GameObject("SlotHolder");
        slotHolder.transform.SetParent(transform);

        for (int i = 0; i < xDim; i++)
            for (int j = 0; j < yDim; j++)
            {
                slots[i, j] = Instantiate(GM.prefabManager.BlockPrefab, slotHolder.transform);
                slots[i, j].SetActive(false);
            }

        return slots;
    }
}
