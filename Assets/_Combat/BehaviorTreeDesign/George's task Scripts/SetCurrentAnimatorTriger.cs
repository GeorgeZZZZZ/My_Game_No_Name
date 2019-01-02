using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	[TaskCategory("George's Script")]
    [TaskDescription("Set this behavior tree obj's animator triger")]
    public class SetCurrentAnimatorTriger : Action
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
                anime.SetTrigger(TrigerName.Value);
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
