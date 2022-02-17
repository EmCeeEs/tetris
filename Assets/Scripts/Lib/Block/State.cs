using System;

using Redux;

public readonly struct BlockState : IState
{
    public readonly bool InvertX;
    public readonly bool InvertY;

    public BlockState(bool invertX = false, bool invertY = false)
    {
        InvertX = invertX;
        InvertY = invertY;
    }

    public readonly static Reducer<BlockState> Reducer = (state, action) =>
        action switch
        {
            InvertXAction _action => new BlockState(!state.InvertX, state.InvertY),
            InvertYAction _action => new BlockState(state.InvertX, !state.InvertY),
            _ => state,
        };

    public readonly static Func<BlockState, bool> GetInvertX = (state) => state.InvertX;
    public readonly static Func<BlockState, bool> GetInvertY = (state) => state.InvertY;
}

public readonly struct InvertXAction : IAction
{
};

public readonly struct InvertYAction : IAction
{
};
