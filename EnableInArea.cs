
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EnableInArea : UdonSharpBehaviour
{

    [Header("Area based enable / disable")]
    [Tooltip("Enables the objects when the user is inside the area, then disables when they leave.")]


    public GameObject[] objects;
    void Start()
    {
        foreach (var item in objects)
        {
            item.SetActive(false);
        }
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player != Networking.LocalPlayer) return;
        Debug.Log("OnPlayerTriggerEnter triggered");
        foreach (var item in objects)
        {
            item.SetActive(true);
        }
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (player != Networking.LocalPlayer) return;
        Debug.Log("OnPlayerTriggerExit triggered");
        foreach (var item in objects)
        {
            item.SetActive(false);
        }
    }
}
