using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public Board board;
    public GameObject joystick;
    public GameObject playButton;
    public TextMeshProUGUI score;

    void Start()
    {
        board = FindObjectOfType<Board>();

        joystick = GameObject.FindWithTag("joystick");
        joystick.SetActive(false);
        playButton = GameObject.FindWithTag("playButton");
        playButton.SetActive(true);
}


    public void StartGame()
    {
        board.isPlaying = true;

        joystick.SetActive(true);
        playButton.SetActive(false);
        board.currentScore = 0;
    }

    public void UpdateScore(float currentScore)
    {
        score.text = "Score: " + currentScore.ToString();
    }
}
