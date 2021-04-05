
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GlobalObjectStateSync : UdonSharpBehaviour
{
    public GameObject objectToSync;
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
        if (objectToSync.activeSelf)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "CurrentlyActive");
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "CurrentlyInactive");
        }
    }

    public void CurrentlyActive()
    {
        if (hasCheckedSynced) return;
        objectToSync.SetActive(true);

        hasCheckedSynced = true;
    }

    public void CurrentlyInactive()
    {
        if (hasCheckedSynced) return;
        objectToSync.SetActive(false);

        hasCheckedSynced = true;
    }
}
