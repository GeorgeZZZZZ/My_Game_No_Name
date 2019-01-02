using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class TestShare : Conditional {
	public SharedTransform AA;
	public SharedTransform BB;
	public override TaskStatus OnUpdate()
        {
            
            Debug.Log("222");
            return TaskStatus.Running;
        }
}
