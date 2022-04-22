
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AudioOcclusionZone : UdonSharpBehaviour
{
    VRCPlayerApi[] _players = new VRCPlayerApi[1];

    [Header("The provided volume settings will be applied that are on the other side of the collider to the user")]

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

    private bool IsLocalPlayerInZone;
    void Start()
    {
        IsLocalPlayerInZone = false;
        UpdatePlayerList();
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if(player == Networking.LocalPlayer)
        {
            IsLocalPlayerInZone = true;
            SetEveryoneToSettings();
        } else
        {
            NonLocalPlayerEnterZone(player);
        }

    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (player == Networking.LocalPlayer)
        {
            IsLocalPlayerInZone = false;
            SetEveryoneDefault();
        }
        else
        {
            NonLocalPlayerExitZone(player);
        }
    }


    public override void OnPlayerTriggerStay(VRCPlayerApi player)
    {
        NonLocalPlayerEnterZone(player);
    }

    private void NonLocalPlayerEnterZone(VRCPlayerApi player)
    {
        if (IsLocalPlayerInZone)
        {
            SetPlayerAudioDefault(player);
        }
        else
        {
            SetPlayerAudioOn(player);
        }

    }

    private void NonLocalPlayerExitZone(VRCPlayerApi player)
    {
        if (IsLocalPlayerInZone)
        {
            SetPlayerAudioOn(player);
        }
        else
        {
            SetPlayerAudioDefault(player);           
        }

    }

    private void SetEveryoneDefault()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            VRCPlayerApi player = _players[i];

            SetPlayerAudioDefault(player);
        }
        
    }

    private void SetEveryoneToSettings()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            VRCPlayerApi player = _players[i];

            SetPlayerAudioOn(player);
        }
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

    private int UpdatePlayerList()
    {
        var playerCount = VRCPlayerApi.GetPlayerCount();
        if (_players == null || _players.Length < playerCount)
        {
            _players = new VRCPlayerApi[playerCount];
        }

        VRCPlayerApi.GetPlayers(_players);
        return playerCount;
    }
    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        UpdatePlayerList();

        if (IsLocalPlayerInZone)
        {
            SetPlayerAudioOn(player);
        }
    }

}
