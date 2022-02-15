using System;
using System.Linq;

using Redux;

public readonly struct State : IState
{
    public readonly int Rotation;
    public readonly bool Inversion;
    public readonly bool[,] Grid;

    public State(int rotation = 0, bool inversion = false, bool[,] grid = null)
    {
        Rotation = rotation;
        Inversion = inversion;
        Grid = grid == null ? grid : new bool[12, 12];
    }
}

public readonly struct Reducer
{
    public static ReducerDelegate<State> root = (state, action) =>
        new State(
            rotate(state.Rotation, action),
            invert(state.Inversion, action),
            grid(state.Grid, action)
        );

    public static ReducerDelegate<int> rotate = (state, action) =>
        action switch
        {
            RotateAction rotateAction => state + rotateAction.Payload,
            _ => state,
        };


    public static ReducerDelegate<bool> invert = (state, action) =>
        action switch
        {
            InvertAction invertAction => !state,
            _ => state,
        };

    public static ReducerDelegate<bool[,]> grid = (state, action) =>
        action switch
        {
            SetSlotAction setSlotAction => Gridd.SetSlot(state, setSlotAction.Payload),
            UnsetSlotAction unsetSlotAction => Gridd.UnsetSlot(state, unsetSlotAction.Payload),
            _ => state,
        };
}


public readonly struct RotateAction : IAction
{
    public readonly int Payload;

    public RotateAction(int numberOfRotations)
    {
        Payload = numberOfRotations;
    }
}

public readonly struct InvertAction : IAction { };

public readonly struct SetSlotAction : IAction
{
    public readonly (int, int) Payload;

    public SetSlotAction(int x, int y)
    {
        Payload = (x, y);
    }
}

public readonly struct UnsetSlotAction : IAction
{
    public readonly (int, int) Payload;

    public UnsetSlotAction(int x, int y)
    {
        Payload = (x, y);
    }
}

public readonly struct Gridd
{
    public readonly static Func<int, int, bool[,]> CreateGrid = (int xDim, int yDim) =>
        new bool[xDim, yDim];

    private readonly static Func<bool[,], bool[,]> CopyState = (state) =>
    {
        int xDim = state.GetLength(0);
        int yDim = state.GetLength(1);
        bool[,] copy = new bool[xDim, yDim];

        for (var i = 0; i < xDim; i++)
        {
            for (var j = 0; j < yDim; j++)
            {
                copy[i, j] = state[i, j];
            }
        }
        return copy;
    };

    public readonly static Func<bool[,], (int X, int Y), bool[,]> SetSlot = (state, slot) =>
    {
        bool[,] copy = CopyState(state);
        copy[slot.X, slot.Y] = true;
        return copy;
    };

    public readonly static Func<bool[,], (int X, int Y), bool[,]> UnsetSlot = (state, slot) =>
    {
        bool[,] copy = CopyState(state);
        copy[slot.X, slot.Y] = false;
        return copy;
    };
}
