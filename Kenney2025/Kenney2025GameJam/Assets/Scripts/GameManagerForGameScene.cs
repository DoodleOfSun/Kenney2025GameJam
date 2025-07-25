using UnityEngine;
using UnityEngine.UI;

public class GameManagerForGameScene : MonoBehaviour
{
    public static GameManagerForGameScene instance;

    public Toggle playerUIToggle;
    public Toggle enemyHPBarToggle;
    public GameObject playerUI;

    public Text playerPowerText;
    public Text playerScoreText;
    public Text leftZombieText;
    public int playerScore;
    public int killCount;

    public GameObject resultPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerUIToggle.isOn = StartInfoManager.instance.isPlayerUI;
        enemyHPBarToggle.isOn = StartInfoManager.instance.isEnemyHPBar;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerScore = 0;
        killCount = 0;
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (killCount >= 100)
        {
            GameEnded();
        }

        Toggle();
        InitText();
    }

    private void Toggle()
    {
        if (playerUIToggle.isOn)
        {
            playerUI.SetActive(true);
        }
        else if (!playerUIToggle.isOn)
        {
            playerUI.SetActive(false);
        }
    }

    private void InitText()
    {
        playerPowerText.text = "Power : " + Player.Instance.power.ToString();
        playerScoreText.text = "Score : " + playerScore.ToString();
        leftZombieText.text = "Zombies Left : " + (100 - killCount).ToString();
    }

    public int powerScore()
    {
        return Player.Instance.power * 500;
    }

    public int killScore()
    {
        return killCount * 1500;
    }

    public void GameEnded()
    {
        AudioManager.instance.AllSourceStop();
        CamFollow.instance.isShake = false;
        Time.timeScale = 0f;
        resultPanel.SetActive(true);
    }
}
