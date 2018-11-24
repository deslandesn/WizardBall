using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour {

    public TextMesh red, blue;

    public void UpdateBoard(int redScore, int blueScore)
    {
        red.text = redScore.ToString();
        blue.text = blueScore.ToString();
    }

    public static ScoreBoardController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


}
