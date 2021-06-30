using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace Assets.Scripts
{
    class MirrorQualityToggle : UdonSharpBehaviour    
    {
        public GameObject Mirror;
        public GameObject OtherObject;


        public void Toggle()
        {
            Mirror.SetActive(!Mirror.activeSelf);

            OtherObject.SetActive(false);
        }

    }
}
