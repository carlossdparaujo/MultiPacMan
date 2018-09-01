using System;
using UnityEngine;

namespace MultiPacMan.Player.Input {
    public class DesktopInputInterpreter : InputInterpreter {

        public override bool IsTurboOn () {
			return UnityEngine.Input.GetAxis("Turbo") > 0f;
        }

        public override Vector2 GetMovementDirection () {
			float xMovement = UnityEngine.Input.GetAxis("Horizontal");
			float yMovement = UnityEngine.Input.GetAxis("Vertical");

            Vector2 movementDir = new Vector2 (xMovement, yMovement);
            return movementDir.normalized;
        }
    }
}