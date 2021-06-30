
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DJBooth : UdonSharpBehaviour
{

    [Header("Player voice")]
    [Tooltip("Adjusts the player volume")]
    [Range(0f, 24f)]
    public float voiceGain = 15f;
    private float voiceFainDefault = 15f;

    [Tooltip("The end of the range for hearing a user's voice")]
    public float voiceFar = 25f;
    private float voiceFarDefault = 25f;

    [Tooltip("The near radius in meters where player audio starts to fall off, it is recommended to keep this at 0")]
    public float voiceNear = 0f;
    private float voiceNearDefault = 0f;

    [Tooltip("The volumetric radius for the player voice, this should be left at 0 unless you know what you're doing")]
    public float voiceVolumetricRadius = 0f;
    private float voiceVolumetricRadiusDefault = 0f;


    void Start()
    {
        
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        Debug.Log("OnPlayerTriggerEnter triggered");
        SetPlayerAudioOn(player);
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        Debug.Log("OnPlayerTriggerExit triggered");
        SetPlayerAudioDefault(player);
    }

    public override void OnPlayerTriggerStay(VRCPlayerApi player)
    {
        Debug.Log("OnPlayerTriggerEnter triggered");
        SetPlayerAudioOn(player);
    }

    private void SetPlayerAudioOn(VRCPlayerApi player)
    {
        player.SetVoiceGain(voiceGain);
        player.SetVoiceDistanceFar(voiceFar);
        player.SetVoiceDistanceNear(voiceNear);
        player.SetVoiceVolumetricRadius(voiceVolumetricRadius);
    }

    private void SetPlayerAudioDefault(VRCPlayerApi player)
    {
        player.SetVoiceGain(voiceFainDefault);
        player.SetVoiceDistanceFar(voiceFarDefault);
        player.SetVoiceDistanceNear(voiceNearDefault);
        player.SetVoiceVolumetricRadius(voiceVolumetricRadiusDefault);
    }
}
