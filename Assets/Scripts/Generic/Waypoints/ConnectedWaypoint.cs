using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Generic.Waypoints
{
    public class ConnectedWaypoint : Waypoint
    {
        [Header("Basic Config:")]
        [Tooltip("The Tag used to idenitfy connected Waypoints")]
        [SerializeField]
        private string WaypointTag = "ConnectedWaypoint";

        [Tooltip("The connectivity radius of Waypoints")]
        [SerializeField]
        private float ConnectivityRadius = 50f;

        private List<ConnectedWaypoint> _connectedWaypoints = new List<ConnectedWaypoint>();

        // Use this for initialization
        void Start()
        {
            UpdateWaypoints();
        }

        private void UpdateWaypoints()
        {
            // Grab all Game Objects in the scene with the Waypoint Tag
            GameObject[] waypoints = GameObject.FindGameObjectsWithTag(WaypointTag);

            // Loop over our Waypoints
            foreach (GameObject gameObject in waypoints)
            {
                ConnectedWaypoint waypoint = gameObject.GetComponent<ConnectedWaypoint>();

                // Ensure that we have a ConnectedWaypoint component
                if (waypoint != null)
                {
                    // Ensure that the connected Waypoint is withing the connectivity radius and that it's not the current Waypoint
                    if (Vector3.Distance(transform.position, waypoint.transform.position) <= ConnectivityRadius && waypoint != this)
                    {
                        _connectedWaypoints.Add(waypoint);
                    }
                }
            }
        }

        public ConnectedWaypoint NextWaypoint(ConnectedWaypoint currentWaypoint)
        {
            // Ensure we have enough waypoints to select the next
            if (_connectedWaypoints.Count >= 2)
            {
                Debug.LogError("NextWaypoint() called, but you don't have enough Waypoints in the Scene.");
                return null;
            }
            else
            {
                ConnectedWaypoint nextWaypoint;
                do
                {
                    int nextIndex = UnityEngine.Random.Range(0, _connectedWaypoints.Count);
                    nextWaypoint = _connectedWaypoints[nextIndex];

                } while (nextWaypoint == currentWaypoint);

                return nextWaypoint;
            }
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _debugSphereRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ConnectivityRadius);
        }
    }
}