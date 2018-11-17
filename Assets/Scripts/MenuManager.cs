using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetPlayerTeam(string value)
    {
        PlayerDataCarrier.instance.SetTeam(value);
        CheckSettings();
    }

    public void SetPlayerPosition(string value)
    {

        PlayerDataCarrier.instance.SetPosition(value);
        CheckSettings();

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CheckSettings()
    {
        if (PlayerDataCarrier.instance.hasPosition && PlayerDataCarrier.instance.hasTeam)
        {
            UnityEngine.UI.Button button = GameObject.Find("StartGameButton").GetComponent<UnityEngine.UI.Button>();
            button.interactable = true;
        }
    }
}