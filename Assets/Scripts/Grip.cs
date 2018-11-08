using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour {

    float pitch;
    float turn;

    Transform marker;

    private void Start()
    {
        marker = transform.Find("InputObj").Find("Marker");
    }

    private void FixedUpdate()
    {
        pitch = marker.localPosition.y;
        turn = marker.localPosition.x;
    }

    public float GetPitch()
    {
        return pitch;
    }

    public float GetTurn()
    {
        return turn;
    }
}
