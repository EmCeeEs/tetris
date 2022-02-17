using Redux;

public readonly struct State : IState
{
    public readonly BoardState Board;
    public readonly BlockState Block;

    public State(BoardState? board = null, BlockState? block = null)
    {
        Board = board ?? new BoardState();
        Block = block ?? new BlockState();
    }
}

public readonly struct Reducer
{
    public static Reducer<State> root = (state, action) =>
        new State(
            BoardState.Reducer(state.Board, action),
            BlockState.Reducer(state.Block, action)
        );
}
