using System.Collections.Generic;

using Redux;
using PolarCoordinates;

public class SetStateAction : IAction
{
    public readonly State State;

    public SetStateAction(State state)
    {
        State = state;
    }
}

public readonly struct AddBlockAction : IAction
{
    public readonly Block block;

    public AddBlockAction(Block newBlock)
    {
        block = newBlock;
    }
}

public readonly struct ResetAction : IAction
{
}

public readonly struct RotateLeftAction : IAction
{
}

public readonly struct RotateRightAction : IAction
{
}


public readonly struct FlagSlotsAction : IAction
{
    public readonly List<GridPoint> Slots;

    public FlagSlotsAction(List<GridPoint> slots)
    {
        Slots = slots;
    }
}

public readonly struct DeleteRowAction : IAction
{
    public readonly int RowNumber;

    public DeleteRowAction(int rowNumber)
    {
        RowNumber = rowNumber;
    }
}
