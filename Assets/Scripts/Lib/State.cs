using Redux;

public readonly struct State : IState
{
    public readonly BoardLogic.State Board;
    public readonly BlockLogic.State Block;

    public State(BoardLogic.State? board = null, BlockLogic.State? block = null)
    {
        Board = board ?? new BoardLogic.State();
        Block = block ?? new BlockLogic.State();
    }
}

public readonly struct Reducer
{
    public static Reducer<State> root = (state, action) =>
        new State(
            BoardLogic.Reducer.Root(state.Board, action),
            BlockLogic.Reducer.Root(state.Block, action)
        );
}
