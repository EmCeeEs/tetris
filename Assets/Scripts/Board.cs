using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Board : MonoBehaviour
{
    public UIHandler uiHandler;
    public bool isPlaying;
    public float currentScore;
    private float singleBlockPoint = 10;
    private float tetrisScore = 100;

    public GameObject BlockPrefab;

    public GameObject currentBlock;
    private GameObject playerBase;

    //[ReadOnly, SerializeField]
    private int rotationState = 0;
    private GameObject[,] slots;

    private const int N_ROWS = 12;
    private const int N_BLOCKS_PER_ROW = 12;
    private float rotationAngle;
    private Slot spawnSlot;
    public PolarGrid grid;

    private BlockSpawner blockSpawner;

    private void Awake()
    {
        uiHandler = FindObjectOfType<UIHandler>();

        playerBase = GameObject.FindWithTag("Base");
        blockSpawner = FindObjectOfType<BlockSpawner>();
        //blockSpawner = GameObject.FindWithTag("BlockSpawner");
        slots = new GameObject[N_ROWS, N_BLOCKS_PER_ROW];
        rotationAngle = 360 / N_BLOCKS_PER_ROW;
        spawnSlot = new Slot(N_ROWS - 4, 0);
        grid = new PolarGrid();
    }

    private void LateUpdate()
    {
        if (isPlaying)
        {
            if (currentBlock == null || currentBlock == blockSpawner.emptyObject)
            {
                currentBlock = blockSpawner.SpawnBlock(spawnSlot);
            }
        }
    }

    public bool IsEmpty(Slot slot)
    {
        if (slot.Scale == -1)
        {
            return false;
        }

        int totalRotation = Utils.Mod(slot.Rotation - rotationState, N_BLOCKS_PER_ROW);
        return slots[slot.Scale, totalRotation] == null;
    }

    public void SetSlot(Slot slot, GameObject block)
    {
        int totalRotation = Utils.Mod(slot.Rotation - rotationState, N_BLOCKS_PER_ROW);

        block.transform.SetParent(playerBase.transform);

        currentScore += singleBlockPoint;
        uiHandler.UpdateScore(currentScore);

        slots[slot.Scale, totalRotation] = block;
    }

    public void CheckForCompleteRows()
    {   
        int offset = 0;
        foreach(int rowNumber in Enumerable.Range(0, N_ROWS))
        {
            int shifted = rowNumber - offset;
            if (IsRowComplete(shifted))
            {
                Debug.Log($"ROW COMPLETED {shifted}");
                RemoveRow(shifted);
                offset++;

                currentScore += tetrisScore;
                uiHandler.UpdateScore(currentScore);
            }
        }
    }


    private bool IsRowComplete(int rowNumber)
    {
        return Enumerable.Range(0, N_BLOCKS_PER_ROW)
            .Select(x => slots[rowNumber, x] != null)
            .All(x => x);
    }


    private void RemoveRow(int rowNumber)
    {
        // remove row
        for (int j=0; j<N_BLOCKS_PER_ROW; j++)
        {
            Destroy(slots[rowNumber, j]);
            slots[rowNumber, j] = null;
        }
        
        // shift other rows
        for (int i=rowNumber+1; i<N_ROWS; i++)
        {
            for (int j=0; j<N_BLOCKS_PER_ROW; j++)
            {
                if (slots[i, j] != null) {
                    slots[i-1, j] = slots[i, j];
                    grid.MoveToSlot(new Slot(i-1, j), slots[i, j]);
                    slots[i, j] = null;
                }
            }
        }
        // LogSlots();
    }

    public void DestroyAll()
    {
        for (int i = 0; i < N_ROWS; i++)
        {
            for (int j = 0; j < N_BLOCKS_PER_ROW; j++)
            {
                Destroy(slots[i, j]);
            }
        }
    }

    public void RotateRight()
    {
        Slot rotationAsSlot = new Slot(0, 1);

        if (CanRotate(currentBlock, rotationAsSlot))
        {
            playerBase.transform.Rotate(Vector3.up, -rotationAngle);
            rotationState -= 1;
        }
    }

    private bool CanRotate(GameObject block, Slot rotationAsSlot)
    {
        // happens if block destroyed but none spawned yet
        if (!block) {
            return true;
        }

        Slot lowerSlot = grid.LowerSlot(block.transform);
        Slot upperSlot = lowerSlot + new Slot(1, 0);

        foreach (Slot layoutSlot in block.GetComponent<BlockParent>().BlockLayout)
        {
            if (!IsEmpty(lowerSlot + layoutSlot + rotationAsSlot) || !IsEmpty(upperSlot + layoutSlot + rotationAsSlot))
            {
                return false;
            }
        }

        return true;
    }

    public void RotateLeft()
    {
        Slot rotationAsSlot = new Slot(0, -1);

        if (CanRotate(currentBlock, rotationAsSlot))
        {
            playerBase.transform.Rotate(Vector3.up, +rotationAngle);
            rotationState += 1;
        }
    }

    private void LogSlots()
    {
        string strMatrix = "";
        for (var i = 0; i<slots.GetLength(0); i++)
        {
            for (var j = 0; j < slots.GetLength(1); j++)
            {
                strMatrix += !IsEmpty(new Slot(i, j));
                strMatrix += " ";
            }
            strMatrix += "\n";
        }
        Debug.Log(strMatrix);
    }
}

