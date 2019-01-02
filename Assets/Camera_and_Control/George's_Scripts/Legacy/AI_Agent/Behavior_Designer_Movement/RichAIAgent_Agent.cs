using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  vr:
 *  - 0.0.1
 *  rewrite RichAIAgent UpdatePath() for assign graphmask parameter
 */

namespace Pathfinding
{
    /*
    // Subclass the RichAI class to provide common functions for the Movement agents
    public class RichAIAgent_Agent : RichAIAgent
    {
        public Path p;
        public int GraphMaskNum = -1; // -1 means all active, 1 means 0b0001, 2 means the 0b0010, 4 means 0b0100, 8 means 0b1000
        public override void UpdatePath()
        {
            CancelCurrentPathRequest();

            waitingForPathCalc = true;
            lastRepath = Time.time;

            OnPathDelegate opd = null;
            seeker.StartPath(tr.position, target.position, opd, GraphMaskNum);

            base.targetReached = false;
        }

        // debug
        protected override void Start()
        {
            base.Start();
        }
    }
    /* */
}