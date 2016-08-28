using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject HelpPanel;

    public void ShowHelp()
    {
        HelpPanel.SetActive(true);
    }

    public void HideHelp()
    {
        HelpPanel.SetActive(false);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
}
