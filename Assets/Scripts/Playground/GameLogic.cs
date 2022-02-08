using System.Linq;
using System; // Array

using UnityEngine; // Debug

public class State
{

    private int _rotationState = 0;
    private bool[,] _blocks;

    public State(int nRows, int nBlocksPerRow)
    {
        _blocks = new bool[nRows, nBlocksPerRow];
    }

    public int NRows()
    {
        return _blocks.GetLength(0);
    }

    public int NBlocksPerRow()
    {
        return _blocks.GetLength(1);
    }

    public void RotateLeft()
    {
        _rotationState = MyUtils.Mod(_rotationState + 1, NBlocksPerRow());
    }

    public void RotateRight()
    {
        _rotationState = MyUtils.Mod(_rotationState - 1, NBlocksPerRow());
    }

    public int GetRotationState()
    {
        return _rotationState;
    }

    public void ActivateBlock(int row, int col)
    {
        _blocks[row, col] = true;
    }

    public void DeactivateBlock(int row, int col)
    {
        _blocks[row, col] = false;
    }

    public bool IsBlockActive(int row, int col)
    {
        return _blocks[row, col];
    }

    public bool IsRowComplete(int rowNumber)
    {
        return Enumerable.Range(0, NBlocksPerRow())
            .Select(x => _blocks[rowNumber, x])
            .All(x => x);
    }

    public void BlowUpRow(int rowNumber)
    {
        _blocks = ClearRowAndShiftArray(rowNumber);
    }

    private bool[,] ClearRowAndShiftArray(int rowNumber)
    {
        bool[,] updatedBlocks = new bool[NRows(), NBlocksPerRow()];

        int nBlocksBeforeRow = NBlocksPerRow() * rowNumber;
        int nBlocksAfterRow = NBlocksPerRow() * (NRows() - rowNumber - 1);

        int iCurrentRow = NBlocksPerRow() * rowNumber; // = nBlocksBeforeRow
        int iNextRow = NBlocksPerRow() * (rowNumber + 1);

        // https://docs.microsoft.com/en-us/dotnet/api/system.array.copy
        Array.Copy(_blocks, 0, updatedBlocks, 0, nBlocksBeforeRow);
        Array.Copy(_blocks, iNextRow, updatedBlocks, iCurrentRow, nBlocksAfterRow);

        return updatedBlocks;
    }
}


public class MyUtils
{
    // helper function implementing modulo operation
    // https://stackoverflow.com/questions/1082917
    public static int Mod(int k, int n)
    {
        return ((k %= n) < 0) ? k + n : k;
    }
}
