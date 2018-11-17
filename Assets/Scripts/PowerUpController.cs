using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public Transform attachPoint;

    //bool HasWand = false;

    public Transform AttachWand(Transform nWand)
    {
        nWand.position = attachPoint.position;
        nWand.rotation = attachPoint.rotation;

        //HasWand = true;

        return attachPoint;
    }


}
