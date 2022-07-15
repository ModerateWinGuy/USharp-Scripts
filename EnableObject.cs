
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EnableObject : UdonSharpBehaviour
{
    public void ShowObject()
    {
        ObjectToShow.SetActive(true);
    }
}
