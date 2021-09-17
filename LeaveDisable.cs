
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class LeaveDisable : UdonSharpBehaviour
{
    public GameObject[] ObjectsToDisable;

    void Start()
    {
        
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (player != Networking.LocalPlayer) return;
        foreach (var item in ObjectsToDisable)
        {
            item.SetActive(false);
        }
    }
}
