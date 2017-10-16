using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicJointsHandler : MonoBehaviour 
{
    public GameObject[] DynamicJoints;

    static GameObject RootDynamicJoints = null;

    [Tooltip("Attach to the parent of this transform")]
    public Transform attachToParentOfTransform = null;

    private void OnEnable()
    {
        if (RootDynamicJoints == null)
            RootDynamicJoints = new GameObject("DynamicJoints");

        var parent = RootDynamicJoints.transform;

        if (attachToParentOfTransform != null)
            parent = attachToParentOfTransform.parent;

        for (int i = 0; i < DynamicJoints.Length; ++i)
        {
            var obj = DynamicJoints[i];

            if (obj == null)
                continue;

            obj.transform.SetParent(parent, true);

            var joint = obj.GetComponent<ConfigurableJoint>();
            if (joint != null)
                joint.connectedAnchor = joint.connectedAnchor;
        }
    }

    private void OnDestroy()
    {
        foreach (var obj in DynamicJoints)
            GameObject.Destroy(obj);

        DynamicJoints = null;
    }
}