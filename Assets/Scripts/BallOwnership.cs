using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOwnership : MonoBehaviour {

    public AIWizard ActiveOwner;
    Rigidbody rb;

    public Zones activeZone;
    
	// Use this for initialization
	void Start () {
        activeZone = Zones.Mid;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeOwner(AIWizard owner)
    {
        if (ActiveOwner != null) {
            ActiveOwner.ballStolen(this.gameObject);
        }
        this.transform.parent = owner.transform;
        this.transform.localPosition = new Vector3(0f, 0f, 0f);
        ActiveOwner = owner;
        rb.isKinematic = true;


    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "BlueZone":
                activeZone = Zones.Blue;
                print("entered blue");
                break;
            case "RedZone":
                activeZone = Zones.Red;
                print("entered red");
                break;
            case "MidZone":
                activeZone = Zones.Mid;
                print("entered mid");
                break;
            case "Target":
                this.transform.position = GameObject.Find("BallReset").transform.position;
                rb.isKinematic = false;
                rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                if (ActiveOwner != null)
                {
                    ActiveOwner.holdingBall = false;
                    ActiveOwner = null;
                }
                this.transform.parent = null;
                break;
        }
    }
    public void passBall(Vector3 position)
    {
        
        ActiveOwner = null;
        this.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce((position - transform.position).normalized * 30, ForceMode.Impulse);
    }
}
public enum Zones
{
    Red, Mid, Blue
}