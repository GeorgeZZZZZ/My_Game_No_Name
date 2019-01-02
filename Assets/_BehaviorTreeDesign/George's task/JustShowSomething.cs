//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("George's Script")]
    [TaskDescription("Debug.Log show some thing")]
    public class JustShowSomething : Action
    {
        public SharedGameObject ShowObjPos;
		public string nameit;
        public override TaskStatus OnUpdate()
        {
			if (ShowObjPos.GetValue() != null){
				Debug.Log(nameit + ", ShowPos: " + ShowObjPos.Value.transform.position);
			}
            return TaskStatus.Running;
        }
    }
}