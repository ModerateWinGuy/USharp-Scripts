
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
        //SyncDrop();

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            var child = ParentObject.transform.GetChild(i);

            var pos = ResetLocation.transform.position;
            Networking.SetOwner(Networking.LocalPlayer, child.gameObject);

            //pos.z += (UnityEngine.Random.Range(0, 30) / 1000);
            child.transform.position = pos;
            child.transform.localRotation = Quaternion.Euler(180, 0, 0);            
        }
    }

    private void SyncDrop()
    {
        for (int i = 0; i < objs.Length; i++)
        {            
            //VRC_Pickup pickup = (VRC_Pickup)objs[i].GetComponent(typeof(VRC_Pickup));

            //if (pickup.IsHeld)
            //{
            //    pickup.Drop();
            //}

            if (Networking.IsOwner(objs[i].gameObject))
            {
                Networking.SetOwner(Networking.LocalPlayer, objs[i].gameObject);
            }
        }
    }
}


