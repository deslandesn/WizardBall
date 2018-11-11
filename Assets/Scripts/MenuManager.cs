using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetPlayerTeam(string value)
    {
        PlayerDataCarrier.instance.SetTeam(value);

    }

    public void SetPlayerPosition(string value)
    {
        
        PlayerDataCarrier.instance.SetPosition(value);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
