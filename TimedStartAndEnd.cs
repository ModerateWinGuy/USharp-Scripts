
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TimedStartAndEnd : UdonSharpBehaviour
{
    public float timerLength;

    public UdonBehaviour startTarget;
    public string startEventName;

    public UdonBehaviour endTarget;
    public string endEventName;

    private bool counting;
    private float _timerCount;

    void Start()
    {
        counting = false;
    }

    public void StartEvent()
    {
        Debug.Log("Starting timed event");
        counting = true;
        startTarget.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, startEventName);      
    }

    private void Update()
    {
        if (!Networking.IsMaster) return;
        if (!counting) return;

        _timerCount += Time.deltaTime;
        
        if(_timerCount > timerLength)
        {
            Debug.Log("Ending timed event");
            endTarget.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, endEventName);
            _timerCount = 0;
            counting = false;
        }
    }
}
