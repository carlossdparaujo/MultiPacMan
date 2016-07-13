using System;
using UnityEngine;

namespace MultiPacMan.Player.Inputs
{
	public class DesktopInputInterpreter : InputInterpreter {

		public override bool IsTurboOn() {
			return Input.GetKey(KeyCode.Space);
		}

		public override Vector3 GetMovementDirection() {
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

			Vector3 movementDir = new Vector3(xMovement, yMovement, 0.0f);
			return movementDir.normalized;
		}
	}
}

