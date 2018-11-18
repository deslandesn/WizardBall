using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    bool Attached = false;
    Transform FollowPos;
    SphereCollider pickupTrigger;

    private void Start()
    {
        pickupTrigger = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Attached)
        {
            transform.position = FollowPos.position;
            transform.rotation = FollowPos.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!Attached)//attach to player
        {
            if(other.tag == "Player")
            {
                FollowPos = other.GetComponentInParent<PowerUpController>().AttachWand(this.transform);
                Attached = true;
                pickupTrigger.enabled = false;
            }
            
        }
    }
}
