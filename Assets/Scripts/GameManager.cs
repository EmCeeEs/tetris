using UnityEngine;
using Redux;

public class GameManager : MonoBehaviour
{
    public Canvas UI;

    public enum STATE { MENU, PLAYING, PAUSE, GAME_OVER }

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

    public Store<State> Store;
    public void StartGame()
    {
        int xDim = 12;
        int yDim = 12;

        MyGrid grid = new MyGrid(xDim, yDim);
        State state = new State();
        Store = new Store<State>(State.Reducer, state, Logger);
    }

    private readonly static Middleware<State> Logger = (store, next) => (action) =>
    {
        Debug.Log($"ACTION: {action}");
        return next(action);
    };
}

public class MyGrid
{
    public readonly (int, int) dimension;

    public MyGrid(int xDim, int yDim)
    {
        dimension = (xDim, yDim);
    }
}
