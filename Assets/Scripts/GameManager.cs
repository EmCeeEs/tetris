using System;

using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// References to other MonoBehaviour scripts
	public UIHandler UIHandler;
	public Board Board;
	public BlockSpawner BlockSpawner;
	public SoundHandler SoundHandler;
	public Player Player;


	public enum GameState { MENU, PLAYING, ANIMATION_PAUSE }
	private GameState gameState = GameState.MENU;

	public Settings Settings;

	[SerializeField]
	private int currentScore;
	public int CurrentScore
	{
		get => currentScore;
		set { currentScore = value; UIHandler.UpdateScore(currentScore); }
	}

	[field: SerializeField]
	public int Speed { get; set; }

	public GameObject currentBlock = null;

	// This is somehow contradictory. Use Singleton but search for normal objects, that
	// should be sigletons as well in oder to make sense. But than everything, including
	// prefab and gameobject instantiation, which leaves the scene black.
	private void Awake()
	{
		UIHandler = FindObjectOfType<UIHandler>();
		Board = FindObjectOfType<Board>();
		BlockSpawner = FindObjectOfType<BlockSpawner>();
		SoundHandler = FindObjectOfType<SoundHandler>();
		Player = FindObjectOfType<Player>();
		Settings = FindObjectOfType<Settings>();
	}

	private void Start()
	{
		Time.timeScale = Settings.Speed.TimeScale;
	}

	private void FixedUpdate()
	{
		if (gameState == GameState.PLAYING)
			UpdateGame();
	}

	private void UpdateGame()
	{
		if (currentBlock == null)
		{
			currentBlock = BlockSpawner.SpawnBlock();
		}
	}

	public void StartGame()
	{
		Board.Clear();
		CurrentScore = 0;
		Speed = 2;

		UIHandler.HideMenu();
		UIHandler.UpdateScore(0);

		Player.gameObject.SetActive(true);
		gameState = GameState.PLAYING;
	}

	public void StartAnimation()
	{
		gameState = GameState.ANIMATION_PAUSE;
	}

	public void StopAnimation()
	{
		gameState = GameState.PLAYING;
	}

	public void EndGame()
	{
		UIHandler.ShowMenu();

		Player.gameObject.SetActive(false);
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
