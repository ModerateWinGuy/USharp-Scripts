
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EntrySound : UdonSharpBehaviour
{
    public AudioSource JoinSound;
    void Start()
    {
        JoinSound.loop = false;
    }

    public override void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player)
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "PlayPlayerJoinedSound");
    }

    public void PlayPlayerJoinedSound()
    {
        JoinSound.Play();
    }
}
