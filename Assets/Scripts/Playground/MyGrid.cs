using UnityEngine;

public class MyGrid
{
    private GameObject[,] slots;

    public MyGrid(uint numberRows, uint numberCols)
    {
        slots = new GameObject[numberRows, numberCols];
    }

    public GameObject GetSlot(MySlot slot)
    {
        return slots[slot.X, slot.Y];
    }

    public void SetSlot(MySlot slot, GameObject go)
    {
        slots[slot.X, slot.Y] = go;
    }

    public void UnsetSlot(MySlot slot)
    {
        slots[slot.X, slot.Y] = null;
    }

    public bool IsEmpty(MySlot slot)
    {
        return slots[slot.X, slot.Y] == null;
    }
}

public readonly struct MySlot
{
    public readonly uint X;
    public readonly uint Y;

    public MySlot(uint x, uint y)
    {
        X = x;
        Y = y;
    } 
}