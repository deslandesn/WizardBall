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

    bool holdingBall;

    Vector3 dirToBall;

    public GameObject targetobj;
    Transform targettrans;
    Vector3 targetDir;

    public enum Team { Blue, Red };
    public Team team;

    public enum Roles { Defense, Mid, Attack};
    public Roles role;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targettrans = GameObject.Find("Ball").transform;
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
            activeSpeed = activeSpeed * 0.8f;
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
            if (team == Team.Blue)
            {
                setNewTarget(GameObject.Find("RedNet/Target"));
            }
            else
            {
                setNewTarget(GameObject.Find("BlueNet/Target"));
            }
        }
    }
    public void setNewTarget(GameObject newTarget)
    {
        targettrans = newTarget.transform;
    }
    public void ballStolen(GameObject ball)
    {
        setNewTarget(ball);
        holdingBall = false;
    }
}