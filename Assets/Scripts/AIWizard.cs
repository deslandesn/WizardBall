using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWizard : MonoBehaviour
{
    private float speed = 0.2f;
    private float speedCap = 8f;
    private float turnSpeed = 2f;


    Transform ballTransform;
    Rigidbody rb;
    Collider collider;

    [HideInInspector]
    public bool holdingBall;
    

    
    public Transform targettrans;

    Vector3 targetDir;

    [HideInInspector]
    public Teams team;

    public GameObject passTarget = null;
    public BallOwnership ball;
    public GameObject zone;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        ball = GameObject.Find("Ball").GetComponent<BallOwnership>();
    }

    private void FixedUpdate()
    {
        
        if (holdingBall)
        {
            if (passTarget != this.transform) {
                //Debug.DrawRay(this.transform.position, passTarget.transform.position - this.transform.position, Color.green);


                print("I should pass to " + passTarget);
                RaycastHit hit;
                // Cast a sphere wrapping character controller 10 meters forward
                // to see if it is about to hit anything.
                if (Physics.SphereCast(transform.position, 10, passTarget.transform.position - this.transform.position, out hit, 20))
                {
                    print(hit.transform.gameObject.name);
                    if(hit.transform.gameObject == passTarget)
                    {
                        Debug.DrawRay(this.transform.position, hit.transform.position - this.transform.position, Color.green);
                        passBall();
                    }
                    else
                    {
                        Debug.DrawRay(this.transform.position, hit.transform.position - this.transform.position, Color.red);
                    }
                }
            }
        }
        if (targettrans == null)
        {
            print("no target");
        }
        float step = turnSpeed * Time.deltaTime;
        float activeSpeed = speed;
        if (holdingBall)
        {
            activeSpeed = activeSpeed * 0.7f;
        }

        transform.position += transform.forward * activeSpeed;
        targetDir = targettrans.position - this.transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball" && holdingBall == false)
        {
            other.gameObject.GetComponent<BallOwnership>().ChangeOwner(this);
            holdingBall = true;
            if (team == Teams.Blue)
            {
                setNewTarget(GameObject.Find("RedNet/Target").transform);
            }
            else
            {
                setNewTarget(GameObject.Find("BlueNet/Target").transform);
            }
        }
    }
    public void setNewTarget(Transform newTarget)
    {
        if (holdingBall) {
            if(newTarget.gameObject.name == "Target")
            targettrans = newTarget;
        }
        else
        {
            targettrans = newTarget;
        }
    }
    public void ballStolen(GameObject ball)
    {
        setNewTarget(ball.transform);
        holdingBall = false;
    }
    public void passBall()
    {
        holdingBall = false;
        ball.passBall(passTarget.transform.position + passTarget.transform.forward);
        //=transform.position+transform.forward
    }
}