using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("George's Tasks")]
    [TaskDescription(@"Calculate distance between two obj and compare with value." +
                      "If distance is smaller than value then return success.")]
    public class CompareDistanceWithDifference : Conditional
    {
        [Tooltip("The first Vector to compare, if comparing obj it self then remain empty")]
        public SharedGameObject FirstObj;
        [Tooltip("The second Vector to compare to")]
        public SharedGameObject SecondObj;
        [Tooltip("If using AstartPathfinding and movement task like seek is using arrive distance then put that value in")]
        public SharedFloat AstarArriveDistance = 0.2f;
        [Tooltip("If the defference between two objs is greater than this value then keep executing, other wise stop and return success.")]
        public SharedFloat DistanceDifference = 0.1f;

        private GameObject firObj, secObj;
        public override void OnStart()
        {
            firObj = GetDefaultGameObject(FirstObj.Value);
            secObj = GetDefaultGameObject(SecondObj.Value);
        }

        public override TaskStatus OnUpdate()
        {
            float _dis = Vector3.Distance(firObj.transform.position, secObj.transform.position) - AstarArriveDistance.Value;
            if (_dis <= DistanceDifference.Value)
               return TaskStatus.Success;
            return TaskStatus.Running;
        }

    }
}