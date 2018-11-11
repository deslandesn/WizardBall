﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataCarrier : MonoBehaviour
{

    string team = "Red", position ="Foward";

    public static PlayerDataCarrier instance = null;

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
        DontDestroyOnLoad(gameObject);
    }

    public void SetTeam(string nTeam)
    {
        team = nTeam;
    }
    public void SetPosition(string nPosition)
    {
        position = nPosition;
    }

    public string GetTeam()
    {
        return team;
    }

    public string GetPosition()
    {
        return position;
    }
}
