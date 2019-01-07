using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskCategory("George's Script")]
    [TaskDescription("Rewrite judgement, if tag box has value then compare tag first(unlike the original task, it never looks tag because it always look layer first).")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}CanSeeObjectIcon.png")]
    public class CanSeeObjectRewrite : CanSeeObject
    {
        [Tooltip("Out put this object to other task, this value only change if target has been found otherwise will remain untouch")]
        public SharedGameObject OutputObject;
        public override TaskStatus OnUpdate()
        {
            if (!string.IsNullOrEmpty(targetTag.Value))
            {
                returnedObject.Value = MovementUtility.WithinSight(transform, offset.Value, fieldOfViewAngle.Value, viewDistance.Value, GameObject.FindGameObjectWithTag(targetTag.Value), targetOffset.Value, ignoreLayerMask, useTargetBone.Value, targetBone);
            }
            else
            {
                base.OnUpdate();
            }

            if (returnedObject.Value != null)
            {
                // only change output value if target has been found
                OutputObject.Value = returnedObject.Value;
                // Return success if an object was found
                return TaskStatus.Success;
            }

            // An object is not within sight so return failure
            return TaskStatus.Failure;
        }
    }
}
