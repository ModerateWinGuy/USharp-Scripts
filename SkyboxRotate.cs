
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SkyboxRotate : UdonSharpBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * (float)0.1);
        //To set the speed, just multiply Time.time with whatever amount you want.
    }
}
