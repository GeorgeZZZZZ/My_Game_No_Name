using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("George's Script")]
    [TaskDescription("Pick up a visible obj")]
    public class PickUpItem : Action
    {
        public SharedGameObject Targe_obj;
        public PropRoot PropRootRight;   //  use to grib obj in right hand
        public PropRoot PropRootLeft;   //  use to grib obj in left hand
        public override TaskStatus OnUpdate()
        {
            if (Targe_obj.Value.GetComponent<Prop>() != null)
            {
                Prop _newProp = Targe_obj.Value.GetComponent<Prop>();
                if (PropRootRight == null || PropRootLeft == null)
                {
                    Debug.LogError("Item no place to put, PropRootRight or PropRootLeft did not assign prop root script");
                    return TaskStatus.Failure;
                }

                // put item in to hand
                if (PropRootRight.currentProp == null) PropRootRight.currentProp = _newProp; 
                else if(PropRootLeft.currentProp == null) PropRootLeft.currentProp = _newProp; 
                else{
                    Debug.Log("Both hand had been occupied.");
                }
                
                return TaskStatus.Success;

            }
            else
            {
                Debug.LogError("Not a valid item, is this obj contain prop script(ex. Prop Melee)?");
            }
            return TaskStatus.Failure;
        }
    }
}
