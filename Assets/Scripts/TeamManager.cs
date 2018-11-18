using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {

    private Transform blueZone, redZone, midZone;
    private Transform blueNet, redNet;


    public AIWizard blueDefence, blueMid, blueOffense;
    public AIWizard redDefence, redMid, redOffense;

    private AIWizard[] redTeam = new AIWizard[3];
    private AIWizard[] blueTeam = new AIWizard[3];
    

    private GameObject ball;
    private BallOwnership ballOwner;


    [HideInInspector]
    public Teams holdingTeam;

    // Use this for initialization
    void Start()
    {
        blueZone = GameObject.Find("BlueZone").transform;
        redZone = GameObject.Find("RedZone").transform;
        midZone = GameObject.Find("MidZone").transform;
        redNet = GameObject.Find("RedNet/Target").transform;
        blueNet = GameObject.Find("BlueNet/Target").transform;

        ball = GameObject.Find("Ball");
        ballOwner = ball.GetComponent<BallOwnership>();



        redTeam[0] = redDefence;
        redTeam[1] = redMid;
        redTeam[2] = redOffense;

        blueTeam[0] = blueDefence;
        blueTeam[1] = blueMid;
        blueTeam[2] = blueOffense;

        holdingTeam = Teams.None;

        
        foreach (AIWizard wizard in redTeam)
        {
            wizard.targettrans = ball.transform;
            wizard.team = Teams.Red;
        }
        foreach (AIWizard wizard in blueTeam)
        {
            wizard.targettrans = ball.transform;
            wizard.team = Teams.Blue;
        }
    }

    // Update is called once per frame
    void Update () {

        /*foreach (AIWizard wizard in redTeam)
        {
            
            if (wizard.holdingBall)
            {
                wizard.passTarget = GetClosestMember(redTeam, blueNet);
                if (holdingTeam != Teams.Red)
                {
                    holdingTeam = Teams.Red;
                }
            }
        }
        foreach (AIWizard wizard in blueTeam)
        {
           
            if (wizard.holdingBall)
            {
                wizard.passTarget = GetClosestMember(blueTeam, redNet);
                if (holdingTeam != Teams.Blue)
                {
                    holdingTeam = Teams.Blue;
                }
            }
        }*/

        if (ballOwner.ActiveOwner != null)
        {
            holdingTeam = ballOwner.ActiveOwner.team;
            ballOwner.ActiveOwner.passTarget = GetClosestMember(holdingTeam == Teams.Red ? redTeam : blueTeam, holdingTeam == Teams.Red ? blueNet : redNet);
        }
        else
        {
            holdingTeam = Teams.None;
        }

        switch (ballOwner.activeZone)
            {
                case Zones.Blue:
                    
                    switch (holdingTeam)
                    {
                        case Teams.Blue:
                            redOffense.setNewTarget(ball.transform);
                            blueDefence.setNewTarget(blueZone);
                            break;
                        case Teams.Red:
                            blueDefence.setNewTarget(ball.transform);
                            redOffense.setNewTarget(blueZone);
                            break;
                        case Teams.None:
                            blueDefence.setNewTarget(ball.transform);
                            redOffense.setNewTarget(ball.transform);
                            break;
                    }


                    blueMid.setNewTarget(midZone);
                    blueOffense.setNewTarget(redZone);

                    redDefence.setNewTarget(redZone);
                    redMid.setNewTarget(midZone);
                    

                    break;
                case Zones.Red:

                    switch (holdingTeam)
                    {
                        case Teams.Blue:
                            blueOffense.setNewTarget(redZone);
                            redDefence.setNewTarget(ball.transform);
                            break;
                        case Teams.Red:
                            blueOffense.setNewTarget(ball.transform);
                            redDefence.setNewTarget(redZone);
                            break;
                        case Teams.None:
                            redDefence.setNewTarget(ball.transform);
                            blueOffense.setNewTarget(ball.transform);
                            break;
                    }


                    blueDefence.setNewTarget(blueZone);
                    blueMid.setNewTarget(midZone);
                    
                    redMid.setNewTarget(midZone);
                    redOffense.setNewTarget(blueZone);
                    break;
                case Zones.Mid:

                    switch (holdingTeam)
                    {
                        case Teams.Blue:
                            blueMid.setNewTarget(midZone);
                            redMid.setNewTarget(ball.transform);
                            break;
                        case Teams.Red:
                            redMid.setNewTarget(midZone);
                            blueMid.setNewTarget(ball.transform);
                            break;
                        case Teams.None:
                            blueMid.setNewTarget(ball.transform);
                            redMid.setNewTarget(ball.transform);
                            break;
                    }


                    blueDefence.setNewTarget(blueZone);
                    blueOffense.setNewTarget(redZone);

                    redDefence.setNewTarget(redZone);
                    redOffense.setNewTarget(blueZone);
                    break;
            }
        
	}
    GameObject GetClosestMember(AIWizard[] Team, Transform Net)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = Net.position;
        foreach (AIWizard t in Team)
        {
            float dist = Vector3.Distance(t.gameObject.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.gameObject.transform;
                minDist = dist;
            }
        }
        return tMin.gameObject;
    }

}
public enum Teams
{
    Red, Blue, None
}