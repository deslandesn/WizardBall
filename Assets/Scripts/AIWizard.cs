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
    public Transform targettrans;

    Vector3 targetDir;
   
    public Teams team;

    //public enum Roles { Defense, Mid, Attack};
   // public Roles role;

    public GameObject zone;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //targettrans = GameObject.Find("Ball").transform;
    }

    private void FixedUpdate()
    {
        if(targettrans == null)
        {
            print("no target");
        }
        float step = turnSpeed * Time.deltaTime;
        float activeSpeed = speed;
        if (holdingBall)
        {
            activeSpeed = activeSpeed * 0.5f;
        }

        transform.position += transform.forward * activeSpeed;
        targetDir = targettrans.position - this.transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        /* targetDir = targettrans.position - this.transform.position;
         targetDir = targetDir.normalized;

         //print(rb.velocity.magnitude);
         rb.AddForce(targetDir * speed);
         if (rb.velocity.magnitude > speedCap)
         {

             rb.velocity = rb.velocity.normalized * speedCap;
         }*/
        //print("moving to ball");
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
}