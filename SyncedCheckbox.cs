using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using UdonSharp;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class SyncedCheckbox : UdonSharpBehaviour
{

    [Header("Synced checkbox for toggling an object on or off for everyone, and keeping the state of the checkbox synced for everyone")]
    [Tooltip("The checkbox to base the setting off")]
    public Toggle toggle;

    [Tooltip("The objects to toggle on and off")]
    public GameObject[] objects;

    [Tooltip("The objects to toggle on and off")]
    public GameObject[] offObjects;

    [Tooltip("If the checkbox & objects are initially on or off")]
    [UdonSynced]
    public bool toggleState;

    private void Start()
    {
        ToggleObjects();
    }


    public override void Interact()
    {
        if (!Networking.IsOwner(toggle.gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, toggle.gameObject);
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);            
        }
            

        ToggleValueChanged(toggle);
    }

    public override void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (Networking.IsClogged)
        {
            if (Networking.IsOwner(toggle.gameObject))
            {
                SendCustomEventDelayedSeconds("_RetrySync", 3, VRC.Udon.Common.Enums.EventTiming.Update);
            }
        }
    }

    public void _RetrySync()
    {
        if (Networking.IsClogged)
        {
            SendCustomEventDelayedSeconds("_RetrySync", 3, VRC.Udon.Common.Enums.EventTiming.Update);
        }
        else
        {
            RequestSerialization();
        }
    }


    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        Debug.Log(change.isOn);
        toggleState = change.isOn;
        RequestSerialization();
        ToggleObjects();
    }

    public override void OnDeserialization()
    {
        if (Networking.IsOwner(toggle.gameObject))
            return;

        toggle.isOn = toggleState;
        ToggleObjects();
    }

    // For turning off checkbox progromatically (IE unlocking empty rooms)
    public void SetToggleOff()
    {
        if (!Networking.IsMaster) return;

        if (!Networking.IsOwner(toggle.gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, toggle.gameObject);
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        toggleState = false;
        SetToggleToSyncState();
    }

    private void SetToggleToSyncState()
    {
        toggle.isOn = toggleState;
    }

    private void ToggleObjects()
    {
        if(objects != null)
        {
            foreach (var item in objects)
            {
                item.SetActive(toggleState);
            }
        }
        if(offObjects != null)
        {
            foreach (var item in offObjects)
            {
                item.SetActive(!toggleState);
            }
        }
    }
}
