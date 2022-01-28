using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System; // Array

public class GameManager : MonoBehaviour
{

    public List<GameObject> BlockParents;
    public GameObject BlockParent; // prefab

    GameObject newBlock;
    public Transform centerOfUnivers;
    public Transform spawnPoint;
    public Transform brickHolder;

    public GameObject initalBlock;
    public State state;

    public float fallingSpeed = 5.0f;
    public float scalingFactor = 1.0f;
    private float timer;
    private float spawnIntervall = 3f;
    public float rotationAmount = 30;
    public List<Transform> toSpawnLocations;
    public GameObject[] blockLevels;

    public GameObject column1;
    public Rigidbody[] column1Slots;  
    public GameObject column2;
    public Rigidbody[] column2Slots;
    public GameObject column3;
    public Rigidbody[] column3Slots;
    public GameObject column4;
    public Rigidbody[] column4Slots;
    public GameObject column5;
    public Rigidbody[] column5Slots;
    public GameObject column6;
    public Rigidbody[] column6Slots;
    public GameObject column7;
    public Rigidbody[] column7Slots;
    public GameObject column8;
    public Rigidbody[] column8Slots;
    public GameObject column9;
    public Rigidbody[] column9Slots;
    public GameObject column10;
    public Rigidbody[] column10Slots;
    public GameObject column11;
    public Rigidbody[] column11Slots;
    public GameObject column12;
    public Rigidbody[] column12Slots;



    public GameObject[,] BLOCKS;

    private void Awake()
    {
        BlockSpawner();
        state = new State(6, 12);
        BLOCKS = new GameObject[6,12];

        column1Slots = column1.GetComponentsInChildren<Rigidbody>();
        column2Slots = column2.GetComponentsInChildren<Rigidbody>();
        column3Slots = column3.GetComponentsInChildren<Rigidbody>();
        column4Slots = column4.GetComponentsInChildren<Rigidbody>();
        column5Slots = column5.GetComponentsInChildren<Rigidbody>();
        column6Slots = column6.GetComponentsInChildren<Rigidbody>();
        column7Slots = column7.GetComponentsInChildren<Rigidbody>();
        column8Slots = column8.GetComponentsInChildren<Rigidbody>();
        column9Slots = column9.GetComponentsInChildren<Rigidbody>();
        column10Slots = column10.GetComponentsInChildren<Rigidbody>();
        column11Slots = column11.GetComponentsInChildren<Rigidbody>();
        column12Slots = column12.GetComponentsInChildren<Rigidbody>();
        for (var i=0;i<6;i++)
        {
            BLOCKS[i, 0] = column1Slots[i].transform.gameObject;
            BLOCKS[i, 1] = column2Slots[i].transform.gameObject;
            BLOCKS[i, 2] = column3Slots[i].transform.gameObject;
            BLOCKS[i, 3] = column4Slots[i].transform.gameObject;
            BLOCKS[i, 4] = column5Slots[i].transform.gameObject;
            BLOCKS[i, 5] = column6Slots[i].transform.gameObject;
            BLOCKS[i, 6] = column7Slots[i].transform.gameObject;
            BLOCKS[i, 7] = column8Slots[i].transform.gameObject;
            BLOCKS[i, 8] = column9Slots[i].transform.gameObject;
            BLOCKS[i, 9] = column10Slots[i].transform.gameObject;
            BLOCKS[i, 10] = column11Slots[i].transform.gameObject;
            BLOCKS[i, 11] = column12Slots[i].transform.gameObject;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > spawnIntervall)
        {
            BlockSpawner();
            timer = 0;
        }
    }

    private void LateUpdate()
    {
        foreach (Transform slot in toSpawnLocations)
        {
            BlockOnBaseSpawner(slot);
        }
        toSpawnLocations.Clear();


        for (var i=0; i<state.nRows(); i++)
        {
            if (state.isRowComplete(i))
            {
                state.blowUpRow(i);
            }
        }


        for (var i = 0; i < state.nRows(); i++)
        {
            for (var j = 0; j < state.nBlocksPerRow(); j++)
            {
              BLOCKS[i, j].SetActive(state._blocks[i, j]);
            }
        }
    }


    public void BlockSpawner()
    {
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        GameObject blockParent = Instantiate(BlockParent, spawnPoint, Quaternion.identity);
        BlockParents.Add(blockParent);
    }

    // Spawn Block on PlayerBase
    public void BlockOnBaseSpawner(Transform currentSlot)
    {
        int blockRotationOffset = Mathf.RoundToInt(currentSlot.parent.eulerAngles.y / 30);
        int level = int.Parse(currentSlot.transform.name) - 1;
        state.activateBlock(level, mod(state.getRotationState()+blockRotationOffset, 12));
        // logBlockState();
    }

    private void logBlockState()
    {
        string strMatrix = "";
        for (var i = 0; i<state.nRows(); i++)
        {
            for (var j = 0; j < state.nBlocksPerRow(); j++)
            {
                strMatrix += state._blocks[i, j];
                strMatrix += " ";
            }
            strMatrix += "\n";
        }
    Debug.Log(strMatrix);
    }

    // helper function to implement modulo operation
    // https://stackoverflow.com/questions/1082917
    private static int mod(int k, int n)
    {
        return ((k %= n) < 0) ? k + n : k;
    }
}


public class State
{

    private int _rotationState = 0;
    public bool[,] _blocks;

    public State(int nRows, int nBlocksPerRow)
    {
        _blocks = new bool[nRows, nBlocksPerRow];
    }

    public int nRows()
    {
        return _blocks.GetLength(0);
    }

    public int nBlocksPerRow()
    {
        return _blocks.GetLength(1);
    }

    public void rotateLeft()
    {
        _rotationState = mod(_rotationState + 1, nBlocksPerRow());
    }

    public void rotateRight()
    {
        _rotationState = mod(_rotationState - 1, nBlocksPerRow());
    }

    public int getRotationState()
    {
        return _rotationState;
    }

    public void activateBlock(int row, int col)
    {
        _blocks[row, col] = true;
    }

    public void deactivateBlock(int row, int col)
    {
        _blocks[row, col] = false;
    }

    public bool isBlockActive(int row, int col)
    {
        return _blocks[row, col];
    }

    public bool isRowComplete(int rowNumber)
    {
        return Enumerable.Range(0, nBlocksPerRow())
            .Select(x => _blocks[rowNumber, x])
            .All(x => x);
    }

    public void blowUpRow(int rowNumber)
    {
        _blocks = clearRowAndShiftArray(rowNumber);
    }

    private bool[,] clearRowAndShiftArray(int rowNumber)
    {
        bool[,] updatedBlocks = new bool[nRows(), nBlocksPerRow()];

        int nBlocksBeforeRow = nBlocksPerRow() * rowNumber;
        int nBlocksAfterRow = nBlocksPerRow() * (nRows() - rowNumber - 1);

        int iCurrentRow = nBlocksPerRow() * rowNumber; // = nBlocksBeforeRow
        int iNextRow = nBlocksPerRow() * (rowNumber + 1);

        // https://docs.microsoft.com/en-us/dotnet/api/system.array.copy
        Array.Copy(_blocks, 0, updatedBlocks, 0, nBlocksBeforeRow);
        Array.Copy(_blocks, iNextRow, updatedBlocks, iCurrentRow, nBlocksAfterRow);

        return updatedBlocks;
    }

    // helper function to implement modulo operation
    // https://stackoverflow.com/questions/1082917
    private static int mod(int k, int n)
    {
        return ((k %= n) < 0) ? k + n : k;
    }
}
