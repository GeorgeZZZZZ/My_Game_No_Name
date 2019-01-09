using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GerogeScripts
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttackAnimationControl : MonoBehaviour
    {
        public float Attack_Frequency_Limit = 1f;
        private float timer = 0f;
        private Animator anime;
        // Start is called before the first frame update
        void Start()
        {
            anime = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            bool leftMousClick = Input.GetMouseButtonDown(0);
            bool rightMousClick = Input.GetMouseButtonDown(1);
            if (timer > 0 )timer -= Time.deltaTime;

            if (timer <= 0f && leftMousClick)
            {
                timer = Attack_Frequency_Limit;
                anime.SetTrigger("IsAttack");
            }
        }
    }
}
