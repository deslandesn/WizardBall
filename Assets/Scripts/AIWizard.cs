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
    [HideInInspector]
    public bool holdingBall;

    [HideInInspector]
    public bool beingPassedTo;

    public bool threatened;
    
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
        ball = GameObject.Find("Ball").GetComponent<BallOwnership>();
        threatened = false;
        beingPassedTo = false;
    }

    private void FixedUpdate()
    {
        
        if (threatened)
        {
            checkPass();
        }
        
        if (targettrans == null)
        {
            print("no target");
        }
        if (beingPassedTo)
        {
            handlePass();
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
    public void handlePass()
    {
        float dist = Vector3.Distance(this.transform.position, targettrans.position);
        print("pass distance is: " + dist);
        if(dist < 0.2f)
        {
            beingPassedTo = false;
            StopCoroutine("TryToCatch");
        }
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
        if (other.gameObject.tag == "Wizard" && holdingBall == true)
        {
            if (other.gameObject.GetComponent<AIWizard>().team != this.team) { 
                threatened = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wizard" && holdingBall == true)
        {
            if (other.gameObject.GetComponent<AIWizard>().team != this.team)
            {
                threatened = false;
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
            if(!beingPassedTo)
            targettrans = newTarget;
        }
    }
    public void ballStolen(GameObject ball)
    {
        setNewTarget(ball.transform);
        holdingBall = false;
        StartCoroutine("BallStun");
    }
    public void passBall()
    {
        holdingBall = false;
        ball.passBall(passTarget.transform.position + passTarget.transform.forward);
        passTarget.GetComponent<AIWizard>().beingPassed(ball.transform);
        //=transform.position+transform.forward
    }
    public void beingPassed(Transform passLocation)
    {
        beingPassedTo = true;
        targettrans = passLocation;
        StartCoroutine("TryToCatch");
    }
    public void checkPass()
    {
        if (holdingBall)
        {
            if (passTarget != this.transform)
            {
                //Debug.DrawRay(this.transform.position, passTarget.transform.position - this.transform.position, Color.green);


                print("I should pass to " + passTarget);
                RaycastHit hit;
                // Cast a sphere wrapping character controller 10 meters forward
                // to see if it is about to hit anything.
                if (Physics.SphereCast(transform.position, 10, passTarget.transform.position - this.transform.position, out hit, 20))
                {
                    print(hit.transform.gameObject.name);
                    if (hit.transform.gameObject == passTarget)
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
    }
    private IEnumerator TryToCatch()
    {
       yield return new WaitForSeconds(3);
        beingPassedTo = false;
        StopCoroutine("TryToCatch");
    }
    private IEnumerator BallStun()
    {
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(3);
        this.GetComponent<Collider>().enabled = true;
        StopCoroutine("BallStun");
    }
}