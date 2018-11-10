using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToPos : MonoBehaviour {

    private void FixedUpdate()
    {
        this.transform.localPosition = Vector3.zero;
    }
}
