using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSwitcher : MonoBehaviour
{

    [SerializeField] private Rigidbody[] rigids;
    [SerializeField] private Animator anim;

    [ContextMenu("Retrieve Rigidbodies")]
    private void RetrieveRigidbodies()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
    }

    [ContextMenu("Clear Ragdoll")]
    private void ClearRagdoll()
    {
        CharacterJoint[] joints = GetComponentsInChildren<CharacterJoint>();
        for (int i = 0; i < joints.Length; i++) 
        {
            DestroyImmediate(joints[i]);
        }
        Rigidbody[] rigidList = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidList.Length; i++)
        {
            DestroyImmediate(rigidList[i]);
        }
        Collider[] colls = GetComponentsInChildren<Collider>();
        for (int i = 0;i < colls.Length; i++)
        {
            DestroyImmediate(colls[i]);
        }
    }
    [ContextMenu("Enable Ragdoll")]
    public void EnableRadoll() => SetRagdoll(true);
    [ContextMenu("Disable Ragdoll")]
    public void DisableRadoll() => SetRagdoll(false);

    private void SetRagdoll(bool ragdollEnable)
    {
        anim.enabled = !ragdollEnable;
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].isKinematic = !ragdollEnable;
        }
    }

}
