using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GerogeScripts
{
    public class AnimationEventMessage : MonoBehaviour
    {
        public event Action<string> Animation_Complete_Event;
        public event Action<string> Animation_middle_00;
        public event Action<string> Animation_middle_01;
		public void GerogeEvent_animation_middle__00 (){
			if (Animation_middle_00 != null) Animation_middle_00(GetAnimationName());
		}
		public void GerogeEvent_animation_middle__01 (){
			if (Animation_middle_01 != null) Animation_middle_01(GetAnimationName());
		}
        public void GeorgeEvent_animation_Complete()
        {
            if (Animation_Complete_Event != null)
            {
                // broadcast the name
                Animation_Complete_Event(GetAnimationName());
            }
        }

        private string GetAnimationName()
        {
            // get animator attached on this obj
            Animator _anime = GetComponent<Animator>();
            // get current animation info
            AnimatorClipInfo[] _anime_info = _anime.GetCurrentAnimatorClipInfo(0);
            // take out animation name
            return _anime_info[0].clip.name;
        }
    }
}
