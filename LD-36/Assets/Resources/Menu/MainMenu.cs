using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject HelpPanel;
    public GameObject RestOfTheScene;

    public void ShowHelp()
    {
        Object.Destroy(RestOfTheScene);
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
