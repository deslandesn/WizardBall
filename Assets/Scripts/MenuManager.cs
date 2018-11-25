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
    readonly string[] times = { "timeThree", "timeFive", "timeTen" };
    readonly string[] scores = { "scoreThree", "scoreFive", "scoreTen" };

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
        string selectedBtn = ConvertFloat(value, "time");
       
        PlayerDataCarrier.instance.SetTime(value);
        ToggleButton(times, selectedBtn);
    }

    public void SetMaxScore(float value)
    {
        string selectedBtn = ConvertFloat(value, "score");
        PlayerDataCarrier.instance.SetScore(value);
        ToggleButton(scores, selectedBtn);
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

    //Dirty workaround for ToggleButtons working with floats... should fix
    public string ConvertFloat(float value, string prefix)
    {
        string convert = value.ToString();
        switch (convert)
        {
            case "3":
                convert = "Three";
                break;
            case "5":
                convert = "Five";
                break;
            case "10":
                convert = "Ten";
                break;
            default:
                break;
        }
        return (prefix + convert);
    }
}