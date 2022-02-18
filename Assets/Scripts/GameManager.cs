using UnityEngine;
using Redux;

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

        CreateState();
    }

    public Store<State> Store;
    private void CreateState()
    {
        State state = new State();
        Store = new Store<State>(Reducer.root, state, Logger);
    }

    private readonly static Middleware<State> Logger = (store, next) => (action) =>
    {
        Debug.Log($"ACTION: {action}");
        return next(action);
    };
}
