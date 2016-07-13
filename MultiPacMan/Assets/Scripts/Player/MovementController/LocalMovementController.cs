using System;
using UnityEngine;
using MultiPacMan.Player.Inputs;
using MultiPacMan.Player.Turbo;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(TurboController))]
	[RequireComponent(typeof(InputInterpreter))]
	public class LocalMovementController : MovementController {
		
		[SerializeField]
		private float speed = 5.0f;
		[SerializeField]
		private float turboSpeed = 8.0f;

		private Vector3 currentVelocity = Vector3.zero;
		private InputInterpreter inputInterpreter;
		private TurboController turboController;

		public override void Start() {
			base.Start();
			inputInterpreter = GetComponent<InputInterpreter>();
			turboController = GetComponent<TurboController>();
		}

		void FixedUpdate() {
			float currentSpeed = turboController.IsTurboOn ? turboSpeed : speed;
			currentVelocity = inputInterpreter.GetMovementDirection()*currentSpeed*Time.fixedDeltaTime;
		}

		void Update() {
			rb.MovePosition(rb.position + new Vector2(currentVelocity.x, currentVelocity.y));
		}
	}
}

