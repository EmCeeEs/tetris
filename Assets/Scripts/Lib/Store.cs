using System;

public class Store
{
    private State state;
    public State GetState()
    {
        return state;
    }

    public delegate void OnDispatchCallback(State state);
    public event EventHandler OnDispatch;

    public Store(State initialState = new State())
    {
        state = initialState;
    }

    public void Dispatch(IAction action)
    {
        state = State.reducer(state, action);
        // call subscribers
        // OnDispatchCallback(state);
    }

    public void Subscribe(OnDispatchCallback callback)
    {
        // OnDispatch += callback;
    }
}

public readonly struct State
{
    public readonly int Rotation;
    public readonly bool Inversion;

    public State(int rotation = 0, bool inversion = false)
    {
        Rotation = rotation;
        Inversion = inversion;
    }

    public static Func<State, IAction, State> reducer =
        (state, action) => new State(
                rotate(state.Rotation, action),
                invert(state.Inversion, action)
            );

    public static Func<int, IAction, int> rotate =
        (state, action) => action is RotateAction
            ? state + ((RotateAction)action).NumberOfRotations
            : state;


    public static Func<bool, IAction, bool> invert =
        (state, action) => action is InvertAction
            ? !state
            : state;
}

public interface IAction { };

public class RotateAction : IAction
{
    public int NumberOfRotations;
}

public class InvertAction : IAction { };
