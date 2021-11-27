using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

public class Teleport : UdonSharpBehaviour
{
    public GameObject targetLocation;

    public void TeleportPlayer()
    {
        Networking.LocalPlayer.TeleportTo(targetLocation.transform.position, targetLocation.transform.rotation);
    }

    public override void Interact()
    {
        Networking.LocalPlayer.TeleportTo(targetLocation.transform.position, targetLocation.transform.rotation);

    }
}
