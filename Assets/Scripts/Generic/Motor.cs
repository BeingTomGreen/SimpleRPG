using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Generic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Motor : MonoBehaviour
    {
        protected NavMeshAgent _navMashAgent;

        // Start is called before the first frame update
        void Start()
        {
            _navMashAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveToTarget(Vector3 target)
        {
            _navMashAgent.SetDestination(target);
        }

        public float RemainingDistance { get { return _navMashAgent.remainingDistance; } }
    }
}