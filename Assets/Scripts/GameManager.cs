using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public UIHandler UIHandler;
	public Board Board;

	public enum GameState { MENU, PLAYING }
	private GameState gameState = GameState.MENU;

	private void Awake()
	{
		UIHandler = FindObjectOfType<UIHandler>();
		Board = FindObjectOfType<Board>();
	}
	private void FixedUpdate()
	{
		if (gameState == GameState.PLAYING)
			DoUpdate();
	}

	private void DoUpdate()
	{
	}

	public void StartGame()
	{
		gameState = GameState.PLAYING;
		Board.isPlaying = true;
		Board.currentScore = 0;
	}

	public void EndGame()
	{
		gameState = GameState.MENU;
		UIHandler.ShowMenu();
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
