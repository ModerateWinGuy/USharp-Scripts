
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ShowForMaster : UdonSharpBehaviour
{
    [Tooltip("The Objects that will be enabled for the master of the instance")]
    public GameObject[] ObjectsToShow;
    void Start()
    {
        EnableForMaster();
    }


    public override void OnPlayerLeft(VRC.SDKBase.VRCPlayerApi player)
    {
        EnableForMaster();
    }

    private void EnableForMaster()
    {
        if(Networking.IsMaster)
        {
            foreach (var item in ObjectsToShow)
            {
                item.SetActive(true);
            }
        }
    }
}
