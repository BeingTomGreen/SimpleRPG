using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Generic.Waypoints
{
    public class Waypoint : MonoBehaviour
    {
        [Header("Debug Config:")]
        [SerializeField]
        protected float _debugSphereRadius = 1f;

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _debugSphereRadius);
        }
    }
}
