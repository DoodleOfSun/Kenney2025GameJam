using UnityEngine;

public class Howtoplay : MonoBehaviour
{
    public GameObject helpPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helpPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenHelp()
    {
        AudioManager.instance.ClickSound();
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {

        AudioManager.instance.ClickSound();
        helpPanel.SetActive(false);
    }
}
