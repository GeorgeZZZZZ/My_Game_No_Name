//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("George's Script")]
    [TaskDescription("If guarding pos or facing pos is empty, then put current position and facing angle to it")]
    public class SetUpGuardPos : Action
    {
        public SharedGameObject GuardingPos;
        public SharedGameObject FacingPos;
        public override TaskStatus OnUpdate()
        {
            if (GuardingPos.GetValue() == null){
                GuardingPos.Value = new GameObject("GudPos");
                GuardingPos.Value.transform.position = transform.position;
            }

            if (FacingPos.GetValue() == null){
                FacingPos.Value = new GameObject("FacingPos");
                FacingPos.Value.transform.position = transform.position + transform.forward;
            }
            
            return TaskStatus.Success;
        }
    }
}