using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

public class TriggerTeleport : UdonSharpBehaviour
{
    public GameObject targetLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        player.TeleportTo(targetLocation.transform.position, targetLocation.transform.rotation);
    }
}
