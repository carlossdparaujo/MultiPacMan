using System;
using UnityEngine;
using MultiPacMan.Player.Input;
using MultiPacMan.Player.Turbo;

namespace MultiPacMan.Player.Movement
{
	public class LocalMovementController : MovementController {

		public delegate bool IsTurboOn();
		public IsTurboOn turboDelegate;

		public delegate Vector2 GetMovementDirection();
		public GetMovementDirection directionDelegate;

		[SerializeField]
		private float speed = 5.0f;
		[SerializeField]
		private float turboSpeed = 8.0f;

		void FixedUpdate() {
			if (directionDelegate == null || turboDelegate == null) {
				return;
			}

			float currentSpeed = turboDelegate() ? turboSpeed : speed;
			currentVelocity = directionDelegate()*currentSpeed*Time.fixedDeltaTime;
		}

		void Update() {
			rb.MovePosition(rb.position + currentVelocity);
		}
	}
}

