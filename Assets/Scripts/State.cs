using System.Collections.Generic;

using Redux;
using PolarCoordinates;

public class State : IState
{
    public readonly int RotationOffset;
    public readonly Block CurrentBlock;
    public readonly bool[,] Grid;

    public State(
        int rotationOffset,
        Block currentBlock,
        bool[,] grid)
    {
        RotationOffset = rotationOffset;
        CurrentBlock = currentBlock;
        Grid = grid;
    }

    public State(State state)
    {
        RotationOffset = state.RotationOffset;
        CurrentBlock = state.CurrentBlock;
        Grid = state.Grid;
    }

    public readonly static Reducer<State> Reducer =
        (state, action) =>
           action switch
            {
                SetStateAction _action => new State(_action.State),
                _ => new State(
                    RotationOffsetRecuder(state.RotationOffset, action),
                    state.CurrentBlock,
                    state.Grid
                ),
            };

    public readonly static Reducer<int> RotationOffsetRecuder =
        (state, action) =>
            action switch
            {
                RotateLeftAction _action => state + 1,
                RotateRightAction _action => state - 1,
                _ => state,
            };

}

public class Block
{
    public readonly List<GridPoint> Layout;
    public readonly Point Position;

    public Block(
        Point position,
        List<GridPoint> layout)
    {
        Position = position;
        Layout = layout;
    }
}
