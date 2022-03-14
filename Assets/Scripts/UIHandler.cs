using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
	private GameManager GM;

	public GameObject Background;
	public GameObject Title;
	public FloatingJoystick Joystick;
	public GameObject GameControlButtons;
	public Button RotateLeft;
	public Button RotateRight;
	public Button PlayButton;
	public Button QuitButton;
	public TextMeshProUGUI Score;

	private Player player;


	private void Awake()
	{
		GM = GameManager.Instance;
		player = FindObjectOfType<Player>();

		//QuitButton.onClick.AddListener(QuitGame);
		//PlayButton.onClick.AddListener(PlayGame);
		//RotateLeft.onClick.AddListener(player.HandleRotationLeft);
		//RotateRight.onClick.AddListener(player.HandleRotationRight);
		Score.gameObject.SetActive(false);

		ShowMenu();
	}
	public void ShowMenu()
	{
		Joystick.gameObject.SetActive(false);
		GameControlButtons.gameObject.SetActive(false);
		PlayButton.gameObject.SetActive(true);
		QuitButton.gameObject.SetActive(true);
		Title.gameObject.SetActive(true);
	}

	public void HideMenu()
	{
		//Joystick.gameObject.SetActive(true);
		GameControlButtons.gameObject.SetActive(true);
		PlayButton.gameObject.SetActive(false);
		QuitButton.gameObject.SetActive(false);
		Title.gameObject.SetActive(false);
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
