using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToForward : MonoBehaviour
{
    public Transform referenceForward;
	// Update is called once per frame
	void Update ()
    {
        this.transform.rotation = referenceForward.rotation;
	}
}
