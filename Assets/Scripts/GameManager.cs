using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// References to other MonoBehaviour scripts
	public UIHandler UIHandler;
	public Board Board;
	public BlockSpawner BlockSpawner;
	public SoundHandler SoundHandler;

	public enum GameState { MENU, PLAYING }
	private GameState gameState = GameState.MENU;

	public GameObject currentBlock = null;

	private void Awake()
	{
		UIHandler = FindObjectOfType<UIHandler>();
		Board = FindObjectOfType<Board>();
		BlockSpawner = FindObjectOfType<BlockSpawner>();
		SoundHandler = FindObjectOfType<SoundHandler>();
	}

	private void FixedUpdate()
	{
		if (gameState == GameState.PLAYING)
			DoUpdate();
	}

	private void DoUpdate()
	{
		if (currentBlock == null)
		{
			currentBlock = BlockSpawner.SpawnBlock();
		}
		if (Board.foundRow)
		{
			Board.disolveTimer += Time.deltaTime;
			Board.disolve.SetFloat("_time", Board.disolveTimer);
		}
	}

	public void StartGame()
	{
		Board.Clear();
		Board.currentScore = 0;

		UIHandler.HideMenu();
		UIHandler.UpdateScore(0);

		gameState = GameState.PLAYING;
	}

	public void EndGame()
	{
		UIHandler.ShowMenu();

		gameState = GameState.MENU;
	}

}

// https://blog.mzikmund.com/2019/01/a-modern-singleton-in-unity/
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

	public static T Instance => LazyInstance.Value;

	private static T CreateSingleton()
	{
		var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
		var instance = ownerObject.AddComponent<T>();
		DontDestroyOnLoad(ownerObject);
		return instance;
	}
}
