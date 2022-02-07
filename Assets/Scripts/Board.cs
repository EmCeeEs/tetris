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

    private GameObject currentBlock;
    private GameObject playerBase;

    private int rotationState = 0;
    private GameObject[,] slots;

    private const int N_ROWS = 10;
    private const int N_BLOCKS_PER_ROW = 12;
    private float rotationAngle;
    private int baseSlot = -1;
    private int spawnSlot;

    private void Awake()
    {
        uiHandler = FindObjectOfType<UIHandler>();

        playerBase = GameObject.FindWithTag("Base");
        slots = new GameObject[N_ROWS, N_BLOCKS_PER_ROW];
        rotationAngle = 360 / N_BLOCKS_PER_ROW;
        spawnSlot = N_ROWS - 1;
    }

    private void LateUpdate()
    {
        if (isPlaying)
        {
            if (currentBlock == null)
            {
                SpawnBlock();
            }
        }  
    }

    public bool IsEmpty(int slot)
    {
        if (slot == baseSlot)
        {
            return false;
        }

        return slots[slot, rotationState] == null;
    }

    public void SetSlot(int slot, GameObject block)
    {
        slots[slot, rotationState] = block;
        block.transform.SetParent(playerBase.transform);

        currentScore += singleBlockPoint;
        uiHandler.UpdateScore(currentScore);

        foreach (int rowNumber in Enumerable.Range(0, N_ROWS))
        {
            if (IsRowComplete(rowNumber))
            {
                Debug.Log("ROW COMPLETED");
                RemoveRow(rowNumber);

                currentScore += tetrisScore;
                uiHandler.UpdateScore(currentScore);
            }
        }

        currentBlock = null;
    }


    private bool IsRowComplete(int rowNumber)
    {
        return Enumerable.Range(0, N_BLOCKS_PER_ROW)
            .Select(x => slots[rowNumber, x])
            .All(x => x);
    }


    private void RemoveRow(int rowNumber)
    {
        // remove row
        for (int j=0; j<N_BLOCKS_PER_ROW; j++)
        {
            Destroy(slots[rowNumber, j]);
        }
        
        // shift other rows
        for (int i=rowNumber+1; i<N_ROWS; i++)
        {
            for (int j=0; j<N_BLOCKS_PER_ROW; j++)
            {
                if (slots[i, j] != null) {
                    slots[i, j].GetComponent<Block>().SetScale(Utils.Slot2Scale(i-1));
                    slots[i-1, j] = slots[i, j];
                    slots[i, j] = null;
                }
            }
        }
    }

    public void RotateRight()
    {
        int nextRotatationState = Utils.Mod(rotationState-1, N_BLOCKS_PER_ROW);
        bool isValid = true;

        if (currentBlock) {
            Block blockScript = currentBlock.GetComponent<Block>();
            int slot = Utils.Scale2Slot(blockScript.GetScale());
            
            isValid = slots[slot, nextRotatationState] == null;
        }

        if (isValid) {
            playerBase.transform.Rotate(Vector3.up, -rotationAngle);
            rotationState = nextRotatationState;
        }
    }

    public void RotateLeft()
    {
        int nextRotatationState = Utils.Mod(rotationState+1, N_BLOCKS_PER_ROW);
        bool isValid = true;

        if (currentBlock) {
            Block blockScript = currentBlock.GetComponent<Block>();
            int slot = Utils.Scale2Slot(blockScript.GetScale());
            
            isValid = slots[slot, nextRotatationState] == null;
        }

        if (isValid) {
            playerBase.transform.Rotate(Vector3.up, rotationAngle);
            rotationState = nextRotatationState;
        }
    }

    public void DestroyAll()
    {
        // shift other rows
        for (int i = 0; i < N_ROWS; i++)
        {
            for (int j = 0; j < N_BLOCKS_PER_ROW; j++)
            {
                Destroy(slots[i, j]);
            }
        }
    }

    public void SpawnBlock()
    {
        if (!IsEmpty(spawnSlot))
        {
            Debug.Log("GAME OVER");
            DestroyAll();
            isPlaying = false;
            uiHandler.joystick.SetActive(false);
            uiHandler.playButton.SetActive(true);
        }
        if (isPlaying)
        {
            currentBlock = Instantiate(BlockPrefab);
            currentBlock.GetComponent<Block>().SetScale(Utils.Slot2Scale(spawnSlot));
        }
    }
}
