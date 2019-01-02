using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GerogeScripts
{
	public class AnimationEventMessage : MonoBehaviour {
		public event Action<string> Animation_Complete_Event;
		public void GeorgeEvent_animation_Complete (){
			if (Animation_Complete_Event != null){
				// get animator attached on this obj
				Animator _anime = GetComponent<Animator>();
				// get current animation info
				AnimatorClipInfo[] _anime_info = _anime.GetCurrentAnimatorClipInfo(0);
				// take out animation name
				string showname = _anime_info[0].clip.name;
				// broadcast the name
				Animation_Complete_Event(_anime_info[0].clip.name);
			}
		}
	}
}
