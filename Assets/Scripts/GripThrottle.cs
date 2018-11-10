using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class GripThrottle : MonoBehaviour
{

    [SteamVR_DefaultActionSet("platformer")]
    public SteamVR_ActionSet actionSet;

    [SteamVR_DefaultAction("Move", "platformer")]
    public SteamVR_Action_Vector2 a_move;

    public float Throttle = 0;

    private SteamVR_Input_Sources hand;
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.activateActionSetOnAttach = actionSet;
    }

    private void FixedUpdate()
    {
        if (interactable.attachedToHand)
        {
            hand = interactable.attachedToHand.handType;
            Vector2 m = a_move.GetAxis(hand);
            Throttle =  m.y;
        }else
        {
            Throttle = 0;
        }
    }

}

