
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleAllColliders : UdonSharpBehaviour
{
    public GameObject[] ObjectColliders;

    void Start()
    {
        ToggleColliders();
    }

    public override void Interact()
    {
        ToggleColliders();
    }

    private void ToggleColliders()
    {
        foreach (var o in ObjectColliders)
        {
            Collider[] colList = o.GetComponentsInChildren<Collider>();

            foreach (Collider collider in colList)
            {
                collider.enabled = !collider.enabled;
            }
        }
    }
}
