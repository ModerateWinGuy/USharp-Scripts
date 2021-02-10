
using System;
using System.Collections;
using UdonSharp;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using VRC.SDKBase;
using VRC.Udon;

public class PostionReset : UdonSharpBehaviour
{
    public GameObject ParentObject;
    public GameObject ResetLocation;

    private GameObject[] objs;


    void Start()
    {
        objs = new GameObject[ParentObject.transform.childCount];

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            var child = ParentObject.transform.GetChild(i);
            objs[i] = child.gameObject;
        }
    }

    public override void Interact()
    {
        ResetItemsPosition();
    }

    public void ResetItemsPosition()
    {

        Networking.SetOwner(Networking.LocalPlayer, ParentObject);

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            var child = ParentObject.transform.GetChild(i);

            var pos = ResetLocation.transform.position;
            Networking.SetOwner(Networking.LocalPlayer, child.gameObject);

            child.transform.position = pos;
            child.transform.localRotation = Quaternion.Euler(180, 0, 0);            
        }
    }

    private void SyncDrop()
    {
        for (int i = 0; i < objs.Length; i++)
        {            
            if (Networking.IsOwner(objs[i].gameObject))
            {
                Networking.SetOwner(Networking.LocalPlayer, objs[i].gameObject);
            }
        }
    }
}


