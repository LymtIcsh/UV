using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCharacterDefaultIK : MonoBehaviour
{
    public Animator at;

    private void OnAnimatorIK(int layerIndex)
    {
        at.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        at.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        at.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
        at.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
    }
}
