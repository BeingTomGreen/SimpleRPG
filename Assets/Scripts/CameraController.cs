using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField]
		[Header("Basic Config:")]
		private Transform Target;

		[SerializeField]
		[Tooltip("Set to Target height to have the camera look at the head rather than feet of the target.")]
		private float Pitch = 2f;        // Pitch up the camera to look at head

		[SerializeField]
		public Vector3 CameraOffset;

		[Header("Zoom Config:")]
		[SerializeField]
		private float ZoomSpeed = 4f;

		[SerializeField]
		private float MinZoom = 5f;

		[SerializeField]
		private float MaxZoom = 15f;

		[SerializeField]
		public float yawSpeed = 100f;

		private float _currentZoom = 10f;
		private float _currentYaw = 0f;

		void Update()
		{
			// Adjust our zoom based on the scrollwheel
			_currentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
			_currentZoom = Mathf.Clamp(_currentZoom, MinZoom, MaxZoom);

			// Adjust our camera's rotation around the player
			_currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
		}

		void LateUpdate()
		{
			// Set our cameras position based on offset and zoom
			transform.position = Target.position - CameraOffset * _currentZoom;

			// Look at the player's head
			transform.LookAt(Target.position + Vector3.up * Pitch);

			// Rotate around the player
			transform.RotateAround(Target.position, Vector3.up, _currentYaw);
		}
	}
}