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
    public static ReducerDelegate<State> root = (state, action)
        => new State(
                rotate(state.Rotation, action),
                invert(state.Inversion, action)
            );

    public static ReducerDelegate<int> rotate = (state, action)
        => action switch
        {
            RotateAction rotateAction => state + rotateAction.Payload,
            _ => state,
        };


    public static ReducerDelegate<bool> invert = (state, action)
        => action switch
        {
            InvertAction invertAction => !state,
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
