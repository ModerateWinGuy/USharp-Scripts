
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GlobalToggleArray : UdonSharpBehaviour
{
    public GameObject[] objects;

    public bool HasBeenToggled;

    private bool hasCheckedSynced;

    void Start()
    {
        hasCheckedSynced = false;
        if (!Networking.LocalPlayer.isMaster)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "QueryMasterForToggle");
        }
    }

    public void QueryMasterForToggle()
    {
        if (HasBeenToggled)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "CurrentlyToggled");
        } else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetHasCheckedSynced");
        }
    }

    public void CurrentlyToggled()
    {
        if (hasCheckedSynced) return;
        Toggle();
        SetHasCheckedSynced();
    }

    public void SetHasCheckedSynced()
    {
        hasCheckedSynced = true;
    }

    public override void Interact()
    {
        GlobalToggle();
    }

    public void GlobalToggle()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Toggle");
    }

    public void Toggle()
    {
        foreach (var item in objects)
        {
            item.SetActive(!item.activeSelf);
        }

        HasBeenToggled = !HasBeenToggled;

    }
}
