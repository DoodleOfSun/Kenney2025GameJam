using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ButtonManager : MonoBehaviour
{
    public GameObject optionPanel;
    public Slider BGMSlider;
    public Slider SFXSlider;

    void Start()
    {
        optionPanel.SetActive(false);
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 1)
        {
            optionPanel.SetActive(true);

            GameObject bgmObj = GameObject.Find("Slider_BGM");
            if (bgmObj != null && bgmObj.GetComponent<Slider>() != null)
                BGMSlider = bgmObj.GetComponent<Slider>();

            GameObject sfxObj = GameObject.Find("Slider_SFX");
            if (sfxObj != null && sfxObj.GetComponent<Slider>() != null)
                SFXSlider = sfxObj.GetComponent<Slider>();


            BGMSlider.value = AudioManager.instance.BGMSource.volume;
            SFXSlider.value = AudioManager.instance.SFXSource1.volume;
            optionPanel.SetActive(false);
        }
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

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
