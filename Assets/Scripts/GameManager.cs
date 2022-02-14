using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game Manager is NULL.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }

    public Store Store;
    private void Start()
    {
        Store = new Store();
    }

}

public class Store
{
    private State state;
    public State GetState()
    {
        return state;
    }

    public Store(State initialState = new State())
    {
        state = initialState;
    }

    public void Dispatch(IBaseAction action)
    {
        state = State.reducer(state, action);
        // call subscribers
    }

    public void Subscribe()
    {
        // TODO
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

    public static Func<State, IBaseAction, State> reducer =
        (state, action) => new State(
                rotate(state.Rotation, (MyAction<int>)action),
                invert(state.Inversion, (MyAction)action)
            );

    public static Func<int, MyAction<int>, int> rotate =
        (state, action) => action.Type == ActionType.ROTATE
            ? state + action.Payload
            : state;


    public static Func<bool, MyAction, bool> invert =
        (state, action) => action.Type == ActionType.INVERT
            ? !state
            : state;
}


public enum ActionType
{
    ROTATE,
    INVERT
}

public readonly struct ActionCreators
{
    public static MyAction<int> CreateRotateAction(int numberOfRotations)
    {
        return new MyAction<int>(ActionType.ROTATE, numberOfRotations);
    }

    public static MyAction CreateInvertAction()
    {
        return new MyAction(ActionType.INVERT);
    }
}

public interface IBaseAction { };

public readonly struct MyAction : IBaseAction
{
    public readonly ActionType Type;

    public MyAction(ActionType type)
    {
        Type = type;
    }
}

public readonly struct MyAction<T> : IBaseAction
{
    public readonly ActionType Type;
    public readonly T Payload;

    public MyAction(ActionType type, T payload)
    {
        Type = type;
        Payload = payload;
    }
}
