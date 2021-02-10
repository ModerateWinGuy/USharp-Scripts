
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EnableForUsernames : UdonSharpBehaviour
{
    public GameObject[] objects;
    public string[] UserNames;
    void Start()
    {
        foreach (var name in UserNames)
        {
            if(Networking.LocalPlayer.displayName == name)
            {
                _EnableObjects();
                return;
            }
        }

        _DisableObjects();        
    }

    private void _EnableObjects()
    {
        Debug.Log($"Enabling objects for ${Networking.LocalPlayer.displayName}");
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }

    private void _DisableObjects()
    {
        Debug.Log($"Disabling objects for ${Networking.LocalPlayer.displayName}");
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
