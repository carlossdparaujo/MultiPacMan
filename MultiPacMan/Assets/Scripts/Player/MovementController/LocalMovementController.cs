using System;
using UnityEngine;
using MultiPacMan.Player.InputInterpreter;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(MovementInputInterpreter))]
	public class LocalMovementController : MovementController {
		
		[SerializeField]
		private float speed = 5.0f;

		private Vector3 currentVelocity = Vector3.zero;
		private MovementInputInterpreter inputInterpreter;

		public override void Start() {
			base.Start();
			inputInterpreter = GetComponent<MovementInputInterpreter>();
		}

		void FixedUpdate() {
			currentVelocity = inputInterpreter.GetMovementDirection()*speed*Time.fixedDeltaTime;
		}

		void Update() {
			rb.MovePosition(rb.position + new Vector2(currentVelocity.x, currentVelocity.y));
		}
	}
}

