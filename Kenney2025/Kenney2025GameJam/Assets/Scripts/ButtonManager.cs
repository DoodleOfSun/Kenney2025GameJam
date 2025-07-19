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

    public void OpenOption()
    {
        AudioManager.instance.ClickSound();
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        AudioManager.instance.ClickSound();
        optionPanel.SetActive(false);
    }

}
