using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIHandler : MonoBehaviour
{
	private GameManager GM;

	public GameObject Panel;
	public FloatingJoystick Joystick;
	public Button PlayButton;
	public Button QuitButton;
	public TextMeshProUGUI Score;

	private void Awake()
	{
		GM = GameManager.Instance;

		QuitButton.onClick.AddListener(QuitGame);
		PlayButton.onClick.AddListener(PlayGame);
		Score.gameObject.SetActive(false);

		ShowMenu();
	}

	public void ShowMenu()
	{
		Panel.SetActive(true);
		Joystick.gameObject.SetActive(false);
		PlayButton.gameObject.SetActive(true);
		QuitButton.gameObject.SetActive(true);
	}

	public void HideMenu()
	{
		Panel.SetActive(false);
		Joystick.gameObject.SetActive(true);
		PlayButton.gameObject.SetActive(false);
		QuitButton.gameObject.SetActive(false);
	}

	public void PlayGame()
	{
		Score.gameObject.SetActive(true);
		GM.StartGame();
	}

	public void QuitGame()
	{
		Debug.Log("QUIT");
		Application.Quit();
	}

	public void UpdateScore(float currentScore)
	{
		Score.text = "Score: " + currentScore.ToString();
	}
}
