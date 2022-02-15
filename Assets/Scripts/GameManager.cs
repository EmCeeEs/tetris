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

    public Store<State> Store;
    private void Start()
    {
        State state = new State();
        Store = new Store<State>(Reducer.root, state);
    }

}
