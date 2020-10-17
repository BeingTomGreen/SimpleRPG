using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Assets.Scripts.Generic.Waypoints;
using System.Collections.Generic;

namespace Assets.Scripts.Generic.Patrol
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Motor))]
    public abstract class Patrol : MonoBehaviour
    {
        [Header("Patrol Details:")]
        [Tooltip("Set the minimum distance the Agent must get to the Waypoint before moving on or waiting.")]
        [SerializeField]
        protected float MinimumWaypointRange;

        [Tooltip("Control whether the Agent should wait at each Waypoint.")]
        [SerializeField]
        protected bool EnableWaiting;

        [Tooltip("How long the Agent should wait at each Waypoint.")]
        [SerializeField]
        protected float WaitTime;

        protected Motor _motor;
        protected bool _isTravelling;
        protected bool _isWaiting;
        protected float _waitTimer;

        protected void Start()
        {
            _motor = GetComponent<Motor>();

            MoveToNextWaypoint();
        }

        // Update is called once per frame
        private void Update()
        {
            // If we're moving and within required range of our waypoint 
            if (_isTravelling && _motor.RemainingDistance <= MinimumWaypointRange)
            {
                _isTravelling = false;

                // If we're waiting at each waypoint
                if (EnableWaiting)
                {
                    _isWaiting = true;
                    _waitTimer = 0f;
                }
                // We are not waiting, so on to the next waypoint
                else
                {
                    MoveToNextWaypoint();
                }
            }

            // If we're waiting
            if (_isWaiting)
            {
                // Add the time since last frame to our timer
                _waitTimer += Time.deltaTime;

                // Check if we've waited enough time
                if (_waitTimer >= WaitTime)
                {
                    _isWaiting = false;
                    MoveToNextWaypoint();
                }
            }
        }

        private void MoveToNextWaypoint()
        {
            Waypoint nextWaypoint = NextWaypoint();
            Vector3 targetVector = nextWaypoint.transform.position;
            _motor.MoveToTarget(targetVector);
            _isTravelling = true;
        }

        protected abstract Waypoint NextWaypoint();
    }
}