using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomStickFollowController : MonoBehaviour {

    public Transform FollowPos;

    private void FixedUpdate()
    {
        transform.LookAt(FollowPos);
    }
}
