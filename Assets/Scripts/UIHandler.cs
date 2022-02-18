using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIHandler : MonoBehaviour
{
    public FloatingJoystick Joystick;
    public Button PlayButton;
    public Button QuitButton;
    public TextMeshProUGUI Score;

    private void Start()
    {
        Joystick.gameObject.SetActive(false);
        QuitButton.onClick.AddListener(QuitGame);
        PlayButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Debug.Log("START");
        Joystick.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
