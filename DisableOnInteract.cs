
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DisableOnInteract : UdonSharpBehaviour
{
    public GameObject objectToDisable;
    void Start()
    {
        
    }

    public override void Interact()
    {
        objectToDisable.SetActive(false);
    }
}
