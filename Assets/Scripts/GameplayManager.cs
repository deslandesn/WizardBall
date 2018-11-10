using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    Transform ballSpawnPos;


    int REDSCORE = 0, BLUESCORE = 0;

    private void Start()
    {
        ballSpawnPos = transform.Find("BallSpawnPos");
    }

    public void ScorePoint(int team)
    {
        switch(team)
        {
            case 0://red team
                REDSCORE++;
                break;
            case 1://blue team
                BLUESCORE++;
                break;
            default:
                break;
        }
    }

    public void ResetBall(Transform ball)
    {
        ball.position = ballSpawnPos.position;
        ball.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
    }


}
