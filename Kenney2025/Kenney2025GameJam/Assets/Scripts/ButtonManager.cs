using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ButtonManager : MonoBehaviour
{
    public GameObject optionPanel;

    void Start()
    {
        optionPanel.SetActive(false);
    }

    public void GameScene()
    {
        AudioManager.instance.ClickSound();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        AudioManager.instance.ClickSound();
        Application.Quit();
    }

    public void GoBackToMainScene()
    {
        AudioManager.instance.ClickSound();
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        AudioManager.instance.ClickSound();
        SceneManager.LoadScene(1);
    }

    public void OpenOption()
    {
        Time.timeScale = 0f;
        AudioManager.instance.ClickSound();
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        Time.timeScale = 1f;
        AudioManager.instance.ClickSound();
        optionPanel.SetActive(false);
    }

}
