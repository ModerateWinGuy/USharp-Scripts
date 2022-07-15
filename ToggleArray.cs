
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleArray : UdonSharpBehaviour
{
    public GameObject[] objects;

    void Start()
    {
    }

    public void Toggle()
    {
        foreach (var item in objects)
        {
            item.SetActive(!item.activeSelf);
        }
    }
}
