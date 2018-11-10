using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripMarkerController : MonoBehaviour
{
    public float followSpeed;

    public Transform followGrip;

	void Start ()
    {

	}
	
	void FixedUpdate ()
    {
        //Follow GrabPoint************************************************
        transform.position = Vector3.Lerp(transform.position,followGrip.position, followSpeed);

        //Limits**********************************************************
        //limit y
        if (transform.localPosition.y > 1f)
        {
            transform.localPosition = 
                new Vector3(transform.localPosition.x, 1f);
        }else if (transform.localPosition.y < -1f)
        {
            transform.localPosition =
                new Vector3(transform.localPosition.x, -1f);
        }

        //limit x
        if (transform.localPosition.x > 1f)
        {
            transform.localPosition =
                new Vector3(1f, transform.localPosition.y);
        }
        else if (transform.localPosition.x < -1f)
        {
            transform.localPosition =
                new Vector3(-1f, transform.localPosition.y);
        }

        //lock against z axis
        transform.localPosition = 
            new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
	}
}
