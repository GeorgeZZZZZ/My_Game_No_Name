using UnityEngine;
using GerogeScripts;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("George's Script")]
    [TaskDescription("Receive and compare anime name with event message from AnimationEventMessage script sending by animation event")]
	public class ReceiveAnimationEvent : Action
    {
		public SharedString CompareAnimationName;
		private AnimationEventMessage anime_event;
		private bool done;

		public override void OnStart() {
			// get AnimationEventMessage script attached on this obj
			anime_event = GetComponent<AnimationEventMessage>();
			if (anime_event == null) Debug.LogWarning("AnimationEventMessage not assign!");
			else{
				anime_event.Animation_Complete_Event += Anime_complete; 
			}
		}
		public override void OnBehaviorComplete () {
			// unsubscribe after this behavior is finish or destroy
			// use null detect is because no matter this task is start or not, it execute this code when behavior tree comlete or destory
			if (anime_event != null) anime_event.Animation_Complete_Event -= Anime_complete;
		}

		public override TaskStatus OnUpdate () {
			if (done) return TaskStatus.Success;
			return TaskStatus.Running;
		}

        public override void OnEnd()
        {
			// reset value after this task successed
            done = false;
			// unsubscribe because OnStart will call every time after this task successed
			if (anime_event != null) anime_event.Animation_Complete_Event -= Anime_complete; 
        }

		private void Anime_complete (string _s){
			// received animation complete message so is done
			if (_s == CompareAnimationName.Value) done = true;
		}

	}
}