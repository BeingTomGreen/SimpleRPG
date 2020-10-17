using UnityEngine;
using System.Collections;
using Assets.Scripts.Generic;
using System;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour
    {
        public Interactable Focus;

        [SerializeField]
        private LayerMask _movementMask;

        private PlayerMotor _playerMotor;
        private Camera _camera;
        private float _maxRaycastDistance = 100f;

        void Start()
        {
            _camera = Camera.main;
            _playerMotor = GetComponent<PlayerMotor>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, _maxRaycastDistance, _movementMask))
                {
                    _playerMotor.MoveToTarget(hit.point);

                    ClearFocus();
                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, _maxRaycastDistance))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }

            }
        }

        public void SetFocus(Interactable newFocus)
        {
            Focus = newFocus;
        }

        public void ClearFocus()
        {
            Focus = null;
        }
    }
}