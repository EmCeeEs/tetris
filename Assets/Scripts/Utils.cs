using System;
using System.Linq;
using System.Collections.Generic;

using PolarCoordinates;

public class Utils
{
    private const int N_ROWS = 12;
    private const int N_COLS = 12;

    public static bool[,] CreateGrid()
        => new bool[N_ROWS, N_COLS];

    public static bool[,] SetSlots(bool[,] grid, List<GridPoint> slots)
    {
        bool[,] copy = CreateGrid();

        Array.Copy(grid, copy, N_ROWS * N_COLS);

        slots.ForEach(slot => copy[slot.X, slot.Y] = true);

        return copy;
    }

    public static bool[,] DeleteRow(bool[,] grid, int rowNumber)
    {
        bool[,] copy = CreateGrid();

        int iFirst = 0;
        int iLast = N_COLS * N_ROWS;

        int iCurrentRow = N_COLS * rowNumber;
        int iNextRow = N_COLS * (rowNumber + 1);

        // https://docs.microsoft.com/en-us/dotnet/api/system.array.copy
        Array.Copy(grid, iFirst, copy, iFirst, iCurrentRow);
        Array.Copy(grid, iNextRow, copy, iCurrentRow, iLast - iNextRow);

        return copy;
    }

    public static bool IsRowComplete(bool[,] grid, int rowNumber)
    {
        return Enumerable.Range(0, N_COLS).All(iCol => grid[rowNumber, iCol]);
    }

    // helper function implementing modulo operation
    // https://stackoverflow.com/questions/1082917
    public static int Mod(int k, int n)
    {
        return ((k %= n) < 0) ? k + n : k;
    }

}
