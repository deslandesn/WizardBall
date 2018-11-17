using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

    Transform ballSpawnPos;

    public GameObject Player;
    public GameObject Wand;

    public Transform[,] SpawnPositions = new Transform[2,3];

    int team =0 , pos=0;


    int REDSCORE = 0, BLUESCORE = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DebugSpawnWand();
        }
    }

    private void Start()
    {
        LoadPositions();
        GetTeamAndPosition();
        ballSpawnPos = transform.Find("BallSpawnPos");

        ResetPlayerPos();
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
        if(REDSCORE > 2|| BLUESCORE >2)
        {
            ReturnToMainMenu();
        }
    }
    
    void ResetPlayerPos()
    {
        Player.transform.position = SpawnPositions[team, pos].position;
        Player.transform.rotation = SpawnPositions[team, pos].rotation;
    }

    void LoadPositions()
    {
        Transform temp = transform.Find("PlayerSpawnPoints");
        SpawnPositions[0, 0] = temp.Find("RF");
        SpawnPositions[0, 1] = temp.Find("RM");
        SpawnPositions[0, 2] = temp.Find("RD");
        SpawnPositions[1, 0] = temp.Find("BF");
        SpawnPositions[1, 1] = temp.Find("BM");
        SpawnPositions[1, 2] = temp.Find("BD");
    }


    void GetTeamAndPosition()
    {
        if(PlayerDataCarrier.instance == null)
        {
            return;
        }
        if(PlayerDataCarrier.instance.GetTeam() == "Red")
        {
            team = 0;
        }else if (PlayerDataCarrier.instance.GetTeam() == "Blue")
        {
            team = 1;
        }

        if (PlayerDataCarrier.instance.GetPosition() == "Forward")
        {
            pos = 0;
        }
        else if (PlayerDataCarrier.instance.GetPosition() == "Mid")
        {
            pos = 1;
        }
        else if (PlayerDataCarrier.instance.GetPosition() == "Defence")
        {
            pos = 2;
        }
    }

    public void ResetBall(Transform ball)
    {
        ball.position = ballSpawnPos.position;
        ball.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void DebugSpawnWand()
    {
        GameObject.Instantiate(Wand, ballSpawnPos.position, ballSpawnPos.rotation);
    }


}
