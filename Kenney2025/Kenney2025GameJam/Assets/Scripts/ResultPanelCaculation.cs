using UnityEngine;
using UnityEngine.UI;

public class ResultPanelCaculation : MonoBehaviour
{
    public Text powerScoreText;
    public Text killScoureText;
    public Text resultScoreText;

    // Update is called once per frame
    void Update()
    {
        powerScoreText.text = "Power * " + Player.Instance.power.ToString() + " = " + GameManagerForGameScene.instance.powerScore().ToString();
        killScoureText.text = "Kill Count * " + GameManagerForGameScene.instance.killCount.ToString() + " = " + GameManagerForGameScene.instance.killScore().ToString();
        int result = GameManagerForGameScene.instance.powerScore() + GameManagerForGameScene.instance.killScore();
        resultScoreText.text = "Result Score : " + result.ToString();
    }
}
