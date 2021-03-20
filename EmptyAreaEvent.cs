using System.Collections;
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EmptyAreaEvent : UdonSharpBehaviour
{    
    public GameObject trigger;              //The trigger to check for the presence of users
    public UdonBehaviour EventTarget; 
    public float recheckInterval = 60;      //How often, in seconds, the script should check for empty
    public string EmptyEventName;           //Name of event to call if no players are found within the region
    public bool EventIsGlobal = true;       //If you want the event sent globally or locally

    private float _timerCount;
    private bool UsersDetected;
    

    void Start()
    {
        UsersDetected = false;
    }

    private void Update()
    {
        if (!Networking.IsMaster) return;
        if (_timerCount < recheckInterval) UsersDetected = false;
        if (_timerCount > recheckInterval + 1)
        {
            if (!UsersDetected)
            {
                Debug.Log("No users Detected, calling event");
                AreaIsEmpty();
            }
            else
            {
                Debug.Log("Users Detected, Not calling event");

            }
            _timerCount = 0;                
        }
        else
        {
            _timerCount += Time.deltaTime;
        }
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
    }

}
