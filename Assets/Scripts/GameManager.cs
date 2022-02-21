using UnityEngine;
using Redux;
using System.Collections.Generic;

using PolarCoordinates;
public class GameManager : MonoBehaviour
{
    public Canvas UI;
    public PrefabManager prefabManager;

    public enum GameState { MENU, PLAYING }
    private GameState gameState = GameState.MENU;

    public Store<State> Store;
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

        Store = new Store<State>(
            State.Reducer,
            null, // no state yet
            Logger
        );
    }

    private void FixedUpdate()
    {
        if (gameState == GameState.PLAYING)
            DoUpdate();
    }

    private readonly Point spawnPoint = new Point(12, 0);
    private readonly Point positionChange = new Point(0.1F, 0);

    private List<GridPoint> layout = new List<GridPoint>() {
        new GridPoint(0, 0),
    };

    private void DoUpdate()
    {
        // if (CanMoveState(state))
        // {
        //     currentBlock = MoveTick(currentBlock);
        // }
        // else
        // {
        //     AddToBoard(currentBlock, board);
        //     CheckForCompleteRows(board);

        //     Block nextBlock = new Block(spawnPoint, layout);
        //     if (CanSpawn(nextBlock, board))
        //     {
        //         currentBlock = nextBlock;
        //     }
        //     else
        //     {
        //         EndGame();
        //     }
        // }
    }

    // private bool CanSpawn(Block block, Board board)
    // {
    //     return board.IsEmpty(spawnPoint);
    // }

    // private Block SpawnBlock(Block block)
    // {
    //     return new Block(spawnPoint, layout);
    // }

    // private static bool CanMove(Block block, Board board)
    // {
    //     return board.IsEmpty(block.Position + positionChange);
    // }

    // private Block MoveTick(Block currentBlock)
    // {
    //     return new Block(currentBlock.Position + positionChange, currentBlock.Layout);
    // }

    // private void AddToBoard(Block currentBlock, Board board)
    // {
    //     GridPoint slot = SnapToGrid(currentBlock.Position);
    //     board.SetActive(slot);
    // }

    private void CheckForCompleteRows(Board board)
    {

    }

    public void StartGame()
    {
        var state = new State(
            0,
            new Block(spawnPoint, layout),
            Utils.CreateGrid()
        );

        Store.Dispatch(new SetStateAction(state));
        gameState = GameState.PLAYING;
    }

    public void EndGame()
    {
        gameState = GameState.MENU;
    }

    private readonly static Middleware<State> Logger = (store, next) => (action) =>
    {
        Debug.Log($"ACTION: {action}");
        return next(action);
    };
}
