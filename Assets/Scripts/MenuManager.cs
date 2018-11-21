using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


    Color selected = Color.red;
    Color available = Color.black;
    UnityEngine.UI.Button btn = null;

    readonly string[] teams = new string[] { "Red", "Blue" };
    readonly string[] positions = { "Defence", "Mid", "Forward" };

    public void SetPlayerTeam(string value)
    {
        PlayerDataCarrier.instance.SetTeam(value);
        ToggleButton(teams, value);
        CheckSettings();
    }

    public void SetPlayerPosition(string value)
    {

        PlayerDataCarrier.instance.SetPosition(value);
        ToggleButton(positions, value);
        CheckSettings();

    }

    public void SetMaxTime(float value)
    {
        Debug.Log(value);
    }

    public void SetMaxScore(float value)
    {
        Debug.Log(value);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    //Check for user team and position
    public void CheckSettings()
    {
        if (PlayerDataCarrier.instance.hasPosition && PlayerDataCarrier.instance.hasTeam)
        {
            UnityEngine.UI.Button button = GameObject.Find("StartGameButton").GetComponent<UnityEngine.UI.Button>();
            button.interactable = true;
        }
    }

    //Toggle button group colors to show selected (red) vs available (black)
    public void ToggleButton(string [] btnGroup, string btnValue)
    {
        foreach (string item in btnGroup)
        {
            btn = GameObject.Find(item).GetComponent<UnityEngine.UI.Button>();
            ColorBlock cb = btn.colors;

            if (item == btnValue)
            {
                cb.normalColor = selected;
                cb.pressedColor = selected;
            }
            else
            {
                cb.normalColor = available;
                cb.pressedColor = available;
            }

            btn.colors = cb;
        }
    }
}