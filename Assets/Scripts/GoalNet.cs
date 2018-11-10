using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalNet : MonoBehaviour
{
    public enum Team { Blue, Red };
    public Team team;

    GameplayManager gameplayMan;

    private void Start()
    {
        gameplayMan = GetComponentInParent<GameplayManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            //assign point
            if(team == Team.Blue)
            {
                gameplayMan.ScorePoint(1);
            }else if(team == Team.Red)
            {
                gameplayMan.ScorePoint(2);
            }

            //reset ball
            gameplayMan.ResetBall(other.transform.parent);
        }
    }
}
