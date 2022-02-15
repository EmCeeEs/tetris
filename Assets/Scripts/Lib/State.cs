public readonly struct State : IState
{
    public readonly int Rotation;
    public readonly bool Inversion;

    public State(int rotation = 0, bool inversion = false)
    {
        Rotation = rotation;
        Inversion = inversion;
    }
}

public readonly struct Reducer
{
    public static ReducerDelegate<State> root =
        (state, action) => new State(
                rotate(state.Rotation, action),
                invert(state.Inversion, action)
            );

    public static ReducerDelegate<int> rotate =
        (state, action) => action is RotateAction
            ? state + ((RotateAction)action).NumberOfRotations
            : state;


    public static ReducerDelegate<bool> invert =
        (state, action) => action is InvertAction
            ? !state
            : state;
}

public class RotateAction : IAction
{
    public int NumberOfRotations;
}

public class InvertAction : IAction { };
