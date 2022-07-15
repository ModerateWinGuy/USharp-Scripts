using System.Collections;
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EmptyAreaEvent : UdonSharpBehaviour
{    
    public UdonBehaviour EventTarget; 
    public float recheckInterval = 10;      //How often, in seconds, the script should check for empty
    public string EmptyEventName;           //Name of event to call if no players are found within the region
    public bool EventIsGlobal = true;       //If you want the event sent globally or locally

    private float _timerCount;
    private bool UsersDetected;
    private bool UsersWhereDetected;
    

    void Start()
    {
        UsersDetected = false;

        SendCustomEventDelayedSeconds("_Recheck", recheckInterval, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    public void _Recheck()
    {
        if (Networking.IsMaster)
        { 
            if (!UsersDetected && UsersWhereDetected)
            {
                UsersWhereDetected = false;
                AreaIsEmpty();
            }
            UsersDetected = false;
        }
        SendCustomEventDelayedSeconds("_Recheck", recheckInterval, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    private void AreaIsEmpty()
    {
        if (EventIsGlobal)
        {
            EventTarget.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, EmptyEventName);
        }
        else
        {
            EventTarget.SendCustomEvent(EmptyEventName);
        }
    }
     
    public override void OnPlayerTriggerStay(VRCPlayerApi player)
    {
        if (!Networking.IsMaster) return;
        UsersDetected = true;
        UsersWhereDetected = true;
    }

}
