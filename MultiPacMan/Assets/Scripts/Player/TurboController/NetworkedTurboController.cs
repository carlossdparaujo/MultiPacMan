using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player.Turbo
{
	public class NetworkedTurboController : TurboController {

		private bool turboOn = false;

		public override float GetTurboFuelPercentage() {
			return 0.0f;
		}

		public override bool IsTurboOn() {
			return turboOn;
		}

		public void UpdateTurbo(Vector2 velocity) {
			turboOn = velocity.magnitude > 0.1f;
		}
	}
}