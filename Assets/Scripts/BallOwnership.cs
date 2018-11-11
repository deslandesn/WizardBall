using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOwnership : MonoBehaviour {

    private AIWizard ActiveOwner;
    
    public Zones activeZone;
    
	// Use this for initialization
	void Start () {
        activeZone = Zones.Mid;

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
        }
    }

}
public enum Zones
{
    Red, Mid, Blue
}