using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using UdonSharp;

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

    public override void Interact()
    {
        if (!Networking.IsOwner(toggle.gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, toggle.gameObject);
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);            
        }
            

        ToggleValueChanged(toggle);
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
        Debug.Log("deserilizationing");
        ToggleObjects();
    }

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
