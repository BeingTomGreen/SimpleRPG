using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Generic
{
    public class Interactable : MonoBehaviour
    {
        [Header("Basic Config:")]
        [SerializeField]
        private float InteractableRadius = 3f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, InteractableRadius);
        }
    }
}