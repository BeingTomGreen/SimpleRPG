using UnityEngine;
using System.Collections;
using Assets.Scripts.Generic.Waypoints;
using System.Collections.Generic;

namespace Assets.Scripts.Generic.Patrol
{
    public class ConnectedWaypointPatrol : Patrol
    {
        private new void Start()
        {

            base.Start();
        }

        protected override Waypoint NextWaypoint()
        {
            throw new System.NotImplementedException();
        }

    }
}