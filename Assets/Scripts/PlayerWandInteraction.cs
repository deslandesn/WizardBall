using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerWandInteraction : MonoBehaviour {

    [SteamVR_DefaultActionSet("platformer")]
    public SteamVR_ActionSet actionSet;

    [SteamVR_DefaultAction("Move", "platformer")]
    public SteamVR_Action_Vector2 a_move;

    float input = 0;

    private SteamVR_Input_Sources hand;
    private Interactable interactable;

    bool used = false;
    GameObject SpawnParent;
    LineRenderer lineRend;

    public Transform FirePos;


    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.activateActionSetOnAttach = actionSet;
        SpawnParent = transform.parent.gameObject;
        lineRend = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (!used)
        {
            if (interactable.attachedToHand)
            {
                hand = interactable.attachedToHand.handType;
                Vector2 m = a_move.GetAxis(hand);
                input = m.y;
            }
            else
            {
                input = 0;
            }
            if(input > 0.8f)
            {
                Fire();
            }
        }else
        {
            if(!interactable.attachedToHand)
            {
                Destroy(SpawnParent);
                Destroy(this.gameObject) ;
            }
            Destroy(SpawnParent, 2f);
            Destroy(this.gameObject, 2f);
        }
    }

    void Fire()
    {
        Ray ray = new Ray(FirePos.position, FirePos.forward);
        RaycastHit hit;

        bool HitSomething = Physics.SphereCast(ray,0.5f,out hit, 1000f);

        if(HitSomething)
        {
            print(hit.transform.name);
            lineRend.SetPosition(0, this.transform.position);
            lineRend.SetPosition(1, hit.point);


        }

        used = true;
    }
}
