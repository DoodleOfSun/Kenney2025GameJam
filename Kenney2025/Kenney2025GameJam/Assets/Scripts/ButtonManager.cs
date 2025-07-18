using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ButtonManager : MonoBehaviour
{
    private AudioSource clickSound;

    public GameObject optionPanel;

    void Start()
    {
        clickSound = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        optionPanel.SetActive(false);
    }

    public void GameScene()
    {
        clickSound.Play();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void OpenOption()
    {
        clickSound.Play();
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        clickSound.Play();
        optionPanel.SetActive(false);
    }

}
