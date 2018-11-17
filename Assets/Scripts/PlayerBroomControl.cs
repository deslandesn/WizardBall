using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBroomControl : MonoBehaviour
{
    public bool NoControllerMode = false;

    public float MaxSpeed;
    public float MovePower;
    public float TurnSpeed;

    float throttle = 0;
    float curPower;

    Transform PitchPos;
    Transform PitchPosReset;
    public Transform a, b, c, d, e, f, g;
    Rigidbody rb;
    
    Grip grip;
    GripThrottle gripThrottle;

    NonVrController nonVrController = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grip = GetComponentInChildren<Grip>();
        PitchPos = transform.Find("VRPlayer").Find("PitchPos");
        PitchPosReset = transform.Find("VRPlayer").Find("PitchPosReset");
        gripThrottle = GetComponentInChildren<GripThrottle>();
        nonVrController = GetComponentInChildren<NonVrController>();

        if (NoControllerMode)
        {
            nonVrController.enabled = true;
        }else//has controllers
        {
            nonVrController.enabled = false;
        }
    }

    private void Update()
    {
        GetControlInputs();
    }

    private void FixedUpdate()
    {
        DoTurn();
        DoPitch();
        PushBroom();
        SpeedLimit();
    }

    /// <summary>
    /// Drive the broom forward
    /// </summary>
    void PushBroom()
    {
        rb.AddForce(PitchPos.forward * curPower, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Get the players desired movement
    /// </summary>
    void GetControlInputs()
    {
        if (NoControllerMode)//no controllers
        {
            throttle = nonVrController.Throttle;
        }
        else//controllers
        {
            throttle = gripThrottle.Throttle;
        }
        curPower = throttle * MovePower;
        if (throttle < 0.2f && throttle > -0.2f)
        {
            curPower = 0;
        }
    }

    /// <summary>
    /// Keep player going less than max speed
    /// </summary>
    void SpeedLimit()
    {
        if(rb.velocity.magnitude > MaxSpeed)//player is going faster than max speed
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;//set velocity to max speed
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    /// <summary>
    /// Turn The Player
    /// </summary>
    void DoTurn()
    {
        if (NoControllerMode)//no controllers
        {
            transform.Rotate(Vector3.up, TurnSpeed * nonVrController.Turn);
        }
        else//controllers
        {
            transform.Rotate(Vector3.up, TurnSpeed * grip.GetTurn());
        }
    }

    /// <summary>
    /// pitch player
    /// </summary>
    void DoPitch()
    {
        Quaternion x = new Quaternion(0,0,0,0);
        if (NoControllerMode)//no controllers
        {
            if (nonVrController.Pitch > 0.7f)
            {
                x = a.rotation;
            }
            else if (nonVrController.Pitch > 0.4f)
            {
                x = b.rotation;
            }
            else if (nonVrController.Pitch > 0.2f)
            {
                x = c.rotation;
            }
            else if (nonVrController.Pitch > -0.05f && nonVrController.Pitch < 0.05f)
            {
                x = d.rotation;
            }
            else if (nonVrController.Pitch > -0.2f)
            {
                x = e.rotation;
            }
            else if (nonVrController.Pitch > -0.4f)
            {
                x = f.rotation;
            }
            else if (nonVrController.Pitch < -0.7f)
            {
                x = g.rotation;
            }
        }
        else//controllers
        {
            if (grip.GetPitch() > 0.7f)
            {
                x = a.rotation;
            }
            else if (grip.GetPitch() > 0.4f)
            {
                x = b.rotation;
            }
            else if (grip.GetPitch() > 0.2f)
            {
                x = c.rotation;
            }
            else if (grip.GetPitch() > -0.05f && grip.GetPitch() < 0.05f)
            {
                x = d.rotation;
            }
            else if (grip.GetPitch() > -0.2f)
            {
                x = e.rotation;
            }
            else if (grip.GetPitch() > -0.4f)
            {
                x = f.rotation;
            }
            else if (grip.GetPitch() < -0.7f)
            {
                x = g.rotation;
            }
        }

        PitchPos.rotation = Quaternion.Lerp(PitchPos.rotation, x, 0.02f);
    }
}
