using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVrController : MonoBehaviour
{
    public KeyCode speedUp, slowDown, up, down, left, right;
    public float Throttle = 0;
    public float Pitch = 0;
    public float Turn = 0;

    private void Update()
    {
        if(Input.GetKey(speedUp))
        {
            Throttle = 1;
        }

        if (Input.GetKey(slowDown))
        {
            Throttle = -1;
        }

        if (!Input.GetKey(slowDown) && !Input.GetKey(speedUp))
        {
            Throttle = 0;
        }

        if (Input.GetKey(up))//pitch 
        {
            Pitch = -1;
        }

        if (Input.GetKey(down))
        {
            Pitch = 1;
        }
        if (!Input.GetKey(down) && !Input.GetKey(up))
        {
            
            Pitch = 0;
        }

        if (Input.GetKey(left))//Rot
        {
            Turn = -1;
        }

        if (Input.GetKey(right))
        {
            Turn = 1;
        }
        if (!Input.GetKey(left) && !Input.GetKey(right))
        {
            Turn = 0;
        }

    }

}
