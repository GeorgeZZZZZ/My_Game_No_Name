//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("George's Script")]
    [TaskDescription("Set this behavior tree obj's animator bool")]
    public class SetCurrentAnimatorBool : Action
    {
		public SharedString TrigerName;
		public bool SetValue;
        private Animator anime;
        public override void OnStart(){
            anime = GetComponent <Animator> ();
        }
        public override TaskStatus OnUpdate()
        {
            if (anime != null){
                anime.SetBool(TrigerName.Value, SetValue);
                return TaskStatus.Success;
            } else
            {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }
        }
        public override void OnReset()
        {
            TrigerName.Value = "";
        }
    }
}