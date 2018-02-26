using System;
using UnityEngine;

namespace MultiPacMan.Player.Inputs
{
	public class DesktopInputInterpreter : InputInterpreter {

		public override bool IsTurboOn() {
			return Input.GetKey(KeyCode.A);
		}

		public override Vector2 GetMovementDirection() {
			float xMovement = 0.0f;
			float yMovement = 0.0f;

			if (Input.GetKey(KeyCode.UpArrow)) {
				yMovement += 1.0f;
			} 

			if (Input.GetKey(KeyCode.DownArrow)) {
				yMovement -= 1.0f;
			} 

			if (Input.GetKey(KeyCode.RightArrow)) {
				xMovement += 1.0f;
			}

			if (Input.GetKey(KeyCode.LeftArrow)) {
				xMovement -= 1.0f;
			}

			Vector2 movementDir = new Vector2(xMovement, yMovement);
			return movementDir.normalized;
		}
	}
}

