using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public UIHandler UIHandler;
	public Board Board;

	public enum GameState { MENU, PLAYING }
	private GameState gameState = GameState.MENU;

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

	private void FixedUpdate()
	{
		if (gameState == GameState.PLAYING)
			DoUpdate();
	}

	private readonly (int, int) spawnPoint = (12, 0);
	private readonly (float, float) positionChange = (0.1F, 0);


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
