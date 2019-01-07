using UnityEngine;
using GerogeScripts;

namespace BehaviorDesigner.Runtime.Tasks
{
    public enum SelectOneEvent
    {
        finish,
        mid_0,
        mid_1
    }

    [TaskCategory("George's Script")]
    [TaskDescription("Receive and compare anime name with event message from AnimationEventMessage script sending by animation event")]
    public class ReceiveAnimationEvent : Action
    {
        public SelectOneEvent event_type;
        public SharedString CompareAnimationName;
        private AnimationEventMessage anime_event;

        private bool done;

        public override void OnStart()
        {
            // get AnimationEventMessage script attached on this obj
            anime_event = GetComponent<AnimationEventMessage>();
            if (anime_event == null) Debug.LogWarning("AnimationEventMessage not assign!");
            else
            {
                switch (event_type)
                {
                    case SelectOneEvent.finish:
                        anime_event.Animation_Complete_Event += Anime_complete;
                        break;
                    case SelectOneEvent.mid_0:
                        anime_event.Animation_middle_00 += Anime_mid_00;
                        break;
                    case SelectOneEvent.mid_1:
                        anime_event.Animation_middle_01 += Anime_mid_01;
                        break;

                }
            }
        }
        public override void OnBehaviorComplete()
        {
            // unsubscribe after this behavior is finish or destroy
            // use null detect is because no matter this task is start or not, it execute this code when behavior tree comlete or destory
			unsubscribe_event();
        }

        public override TaskStatus OnUpdate()
        {
            if (done) return TaskStatus.Success;
            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            // reset value after this task successed
            done = false;
            // unsubscribe because OnStart will call every time after this task successed
			unsubscribe_event();
        }

        private void Anime_complete(string _s)
        {
            // received animation complete message so is done
            if (_s == CompareAnimationName.Value) done = true;
        }

        private void Anime_mid_00(string _s)
        {
            // received animation complete message so is done
            if (_s == CompareAnimationName.Value) done = true;
        }
        private void Anime_mid_01(string _s)
        {
            // received animation complete message so is done
            if (_s == CompareAnimationName.Value) done = true;
        }

        private void unsubscribe_event()
        {
            if (anime_event == null) return;

            switch (event_type)
            {
                case SelectOneEvent.finish:
                    anime_event.Animation_Complete_Event -= Anime_complete;
                    break;
                case SelectOneEvent.mid_0:
                    if (anime_event != null)
                        anime_event.Animation_middle_00 -= Anime_mid_00;
                    break;
                case SelectOneEvent.mid_1:
                    if (anime_event != null)
                        anime_event.Animation_middle_01 -= Anime_mid_01;
                    break;

            }
        }
    }
}