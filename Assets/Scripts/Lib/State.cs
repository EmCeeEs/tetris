using Redux;

public readonly struct State : IState
{
    public readonly BoardState Board;
    public readonly BlockState Block;

    public State(BoardState board, BlockState block)
    {
        Board = board;
        Block = block;
    }

    public readonly static Reducer<State> Reducer = (state, action) =>
        new State(
            BoardState.Reducer(state.Board, action),
            BlockState.Reducer(state.Block, action)
        );
}
