using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using UdonSharp;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class SyncedToggle : UdonSharpBehaviour
{

    [Header("Synced toggling objects on or off for everyone")]


    [Tooltip("The objects to toggle on and off - initially On")]
    public GameObject[] objects;

    [Tooltip("The objects to toggle on and off - initially off")]
    public GameObject[] offObjects;

    [Tooltip("If the objects are initially on or off")]
    [UdonSynced]
    public bool toggleState;

    public bool RequireMaster;



    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);            
        }

        if (RequireMaster)
        {
            if (Networking.IsMaster ) {
                toggleState = !toggleState;
                RequestSerialization();
                ToggleObjects();
            }
        }
        else
        {
            toggleState = !toggleState;
            RequestSerialization();
            ToggleObjects();
        }
        Debug.Log(toggleState);
    }


    public override void OnDeserialization()
    {
        ToggleObjects();
    }

    private void ToggleObjects()
    {
        foreach (var item in objects)
        {
            item.SetActive(toggleState);
        }
        foreach (var item in offObjects)
        {
            item.SetActive(!toggleState);
        }
    }
}
