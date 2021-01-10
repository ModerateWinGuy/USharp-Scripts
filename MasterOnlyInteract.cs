
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MasterOnlyInteract : UdonSharpBehaviour
{

    public string EventName;
    public UdonBehaviour target;

    void Start()
    {
        
    }

    public override void Interact()
    {
        if (Networking.IsMaster)
        {
            target.SendCustomEvent(EventName);
        }
    }

}
