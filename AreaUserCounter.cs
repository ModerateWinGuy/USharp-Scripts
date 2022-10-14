using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class AreaUserCounter : UdonSharpBehaviour
{
    [Header("Trigger region user counter")]
    private GameObject trigger;

    [Tooltip("The UI text field that will display the amount of users")]
    public Text textField;

    [Tooltip("How often you want the counter to recount the users in the trigger area")]
    public float recheckInterval = 100;

    private bool _resetting;
    private float startY;

    private int UserCount;

    void Start()
    {
        trigger = this.gameObject;
        startY = trigger.transform.position.y;
        _resetting = false;

        SendCustomEventDelayedSeconds("MoveAreaAreaDown", recheckInterval, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    public void MoveAreaAreaDown()
    {
        _resetting = true;
        //Move trigger low into the void
        var pos = trigger.transform.position;
        pos.y = startY - 100;
        trigger.transform.position = pos;

        SendCustomEventDelayedFrames("ResetArea", 5, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    public void ResetArea()
    {
        // Make sure counter reads zero
        UserCount = 0;
        _resetting = false;
        // Move the trigger pack into position, which will trigger on enter events for all the users in the region
        var pos = trigger.transform.position;
        pos.y = startY;
        trigger.transform.position = pos;

        UpdateCounter();
        SendCustomEventDelayedSeconds("MoveAreaAreaDown", recheckInterval, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    private void UpdateCounter()
    {
        if (textField.text == UserCount.ToString()) return;

        //Debug.Log("Updating Text Field");
        textField.text = UserCount.ToString();
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        //Debug.Log("OnPlayerTriggerEnter triggered");
        UserCount++;
        UpdateCounter();
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (_resetting) return;

        //Debug.Log("OnPlayerTriggerExit triggered");
        UserCount--;
        UpdateCounter();
    }

    public override void OnDeserialization()
    {
        UpdateCounter();
    }

}
