
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickupMicrophone : UdonSharpBehaviour
{

    [Header("Microphone voice amp settings")]
    [Tooltip("Adjusts the holding player's volume")]
    [Range(0f, 24f)]
    public float voiceGain = 15f;
    private float voiceGainDefault = 15f;

    [Tooltip("The end of the range for hearing a user's voice")]
    public float voiceFar = 25f;
    private float voiceFarDefault = 25f;

    [Tooltip("The near radius in meters where player audio starts to fall off, it is recommended to keep this at 0")]
    public float voiceNear = 0f;
    private float voiceNearDefault = 0f;

    [Tooltip("The volumetric radius for the player voice, this should be left at 0 unless you know what you're doing")]
    public float voiceVolumetricRadius = 0f;
    private float voiceVolumetricRadiusDefault = 0f;

    private VRCPlayerApi playerHoldingMic; 


    void Start()
    {
        
    }

    public void EnablePickup()
    {
        this.GetComponent<Collider>().enabled = true;
    }


    public void EnableMicrophone()
    {        
        SetPlayerAudioOn(Networking.GetOwner(this.gameObject));
    }

    public void DisableMicrophone()
    {
        SetPlayerAudioDefault(Networking.GetOwner(this.gameObject));
    }

    public override void OnOwnershipTransferred(VRCPlayerApi player) 
    {
        Debug.Log($"Microphone ownership transferred to: {player.displayName}");
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DisableMicrophone");
    }

    public override void OnPickupUseDown() {
        if (Networking.GetOwner(this.gameObject) != Networking.LocalPlayer) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);

        Debug.Log("PickupUseDown");
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "EnableMicrophone");
    }
    public override void OnPickupUseUp() {
        Debug.Log("OnPickupUseUp");

        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DisableMicrophone");
    }

    public override void OnPickup() {
        Debug.Log("OnPickup");
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DisableMicrophone");
    }

    public override void OnDrop() {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DisableMicrophone");
        Debug.Log("OnDrop");
    }


    private void SetPlayerAudioOn(VRCPlayerApi player)
    {
        Debug.Log("Turning player up");

        player.SetVoiceGain(voiceGain);
        player.SetVoiceDistanceFar(voiceFar);
        player.SetVoiceDistanceNear(voiceNear);
        player.SetVoiceVolumetricRadius(voiceVolumetricRadius);
    }

    private void SetPlayerAudioDefault(VRCPlayerApi player)
    {
        Debug.Log("Turning player Down");

        player.SetVoiceGain(voiceGainDefault);
        player.SetVoiceDistanceFar(voiceFarDefault);
        player.SetVoiceDistanceNear(voiceNearDefault);
        player.SetVoiceVolumetricRadius(voiceVolumetricRadiusDefault);
    }
}
