using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartInfoManager : MonoBehaviour
{
    public static StartInfoManager instance;
    private Toggle playerUI;
    private Toggle enemyHPBar;

    public bool isPlayerUI;
    public bool isEnemyHPBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        playerUI = GameObject.Find("Toggle_PlayerUI").GetComponent<Toggle>();
        enemyHPBar = GameObject.Find("Toggle_EnemyHPBar").GetComponent<Toggle>();



        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerUI = playerUI.isOn;
        isEnemyHPBar = enemyHPBar.isOn;
    }
}
