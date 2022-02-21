using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIHandler : MonoBehaviour
{
    public FloatingJoystick Joystick;
    public Button PlayButton;
    public Button QuitButton;
    public TextMeshProUGUI Score;

    private GameManager GM;

    private void Start()
    {
        GM = GameManager.Instance;

        QuitButton.onClick.AddListener(QuitGame);
        PlayButton.onClick.AddListener(StartGame);

        ShowMenu();
    }

    public void ShowMenu()
    {
        Joystick.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        Joystick.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        HideMenu();
        GM.StartGame();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
