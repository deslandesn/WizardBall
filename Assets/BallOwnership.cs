using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOwnership : MonoBehaviour {

    private AIWizard ActiveOwner;
	// Use this for initialization
	void Start () {
		
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
}
