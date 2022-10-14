using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using UnityEngine.UI;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class SyncedAnimation : UdonSharpBehaviour
{
    [UdonSynced]
    public bool toggleState;
    public bool RequireMaster;
    public string StateName;
    public Animator anim;

    void Start()
    {
        anim.SetBool(StateName, toggleState);
    }

    public override void Interact()
    {
        if (RequireMaster)
        {
            if (Networking.IsMaster) {
                setMaster();
                toggleState = !toggleState;
                RequestSerialization();
                SetState();
            }
        }
        else
        {
            setMaster();
            toggleState = !toggleState;
            RequestSerialization();
            SetState();
        }
    }

    private void setMaster()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
    }


    public override void OnDeserialization()
    {
        SetState();
    }

    private void SetState()
    {

        anim.SetBool(StateName, toggleState);
    }
}
