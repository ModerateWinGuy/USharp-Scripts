using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class AreaUserCounter : UdonSharpBehaviour
{
    [Header("Trigger region user counter")]

    [Tooltip("The trigger to count the users within.")]
    public GameObject trigger;

    [Tooltip("The UI text field that will display the amount of users")]
    public Text textField;

    [Tooltip("How often you want the counter to recount the users in the trigger area")]
    public float recheckInterval = 100;

    private bool _resetting;
    private float _timerCount;
    private float startY;

    [UdonSynced]
    public int UserCount;

    void Start()
    {
        startY = trigger.transform.position.y;
        _resetting = false;
    }

    private void Update()
    {
        if (!Networking.IsMaster) return;

        if (_timerCount >= recheckInterval)
        {
            if (_resetting)
            {
                if (_timerCount > recheckInterval + 1)
                {                    
                    Debug.Log("Returning Trigger to default Pos");

                    // Move the trigger pack into position, which will trigger on enter events for all the users in the region
                    var pos = trigger.transform.position;
                    pos.y = startY;
                    trigger.transform.position = pos;

                    _timerCount = 0;
                    _resetting = false;
                }
                else
                {
                    _timerCount += Time.deltaTime;
                }
            }
            else
            {
                Debug.Log("Moving Trigger area up");
                _resetting = true;
                
                //Move trigger low into the void
                var pos = trigger.transform.position;
                pos.y = startY - 100;
                trigger.transform.position = pos;

                // Make sure counter reads zero
                UserCount = 0;
            }
            UpdateCounter();
        }
        else
        {
            _timerCount += Time.deltaTime;
        }
    }

    private void UpdateCounter()
    {      
        if (textField.text == UserCount.ToString()) return;

        Debug.Log("Updating Text Field");
        textField.text = UserCount.ToString();
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!Networking.IsMaster) return;

        Debug.Log("OnPlayerTriggerEnter triggered");
        UserCount++;
        UpdateCounter();
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (!Networking.IsMaster || _resetting) return;

        Debug.Log("OnPlayerTriggerExit triggered");
        UserCount--;
        UpdateCounter();
    }

    public override void OnDeserialization()
    {
        UpdateCounter();
    }
}
