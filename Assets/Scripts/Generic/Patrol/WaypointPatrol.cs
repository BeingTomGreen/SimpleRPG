using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Assets.Scripts.Generic.Waypoints;

namespace Assets.Scripts.Generic.Patrol
{
    public class WaypointPatrol : Patrol
    {
        [Header("Waypoint Patrol Details:")]
        [Tooltip("Control whether the Agent should switch direction.")]
        [SerializeField]
        private bool EnableDirectionSwitch;

        [Tooltip("The probability that the Agent will switch direction.")]
        [SerializeField]
        private float DirectionSwitchProbability;

        [Tooltip("Waypoints for the Agent to patrol.")]
        [SerializeField]
        protected List<Waypoint> Waypoints;

        private bool _patrollingForward = true;
        private int _currentWaypointIndex = 0;

        private void SwapDirection()
        {
            _patrollingForward = !_patrollingForward;
            Debug.Log(gameObject.name + " switched patrol direction.");
        }

        protected override Waypoint NextWaypoint()
        {
            // Check if we need to change direction
            if (EnableDirectionSwitch && UnityEngine.Random.Range(0f, 1f) <= DirectionSwitchProbability)
            {
                SwapDirection();
            }

            // We're moving forwards
            if (_patrollingForward)
            {
                // Increment waypoint, but set back to 0 if more than Waypoints.Count
                _currentWaypointIndex = (_currentWaypointIndex + 1) % Waypoints.Count;
            }
            // Backwards
            else
            {
                // Decrement our index, and if less than 0..
                if (--_currentWaypointIndex < 0)
                {
                    // ... Go to the first
                    _currentWaypointIndex = Waypoints.Count - 1;
                }
            }

            // Return the current waypoint
            return Waypoints[_currentWaypointIndex];
        }
    }
}