using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player.Turbo
{
	public class NetworkedTurboController : TurboController {
		public delegate Vector2 GetVelocity();
		public GetVelocity getVelocityDelegate;

		public override float GetTurboFuelPercentage() {
			return 0.0f;
		}

		public override bool IsTurboOn() {
			return getVelocityDelegate().magnitude / Time.fixedDeltaTime > 5.0f;
		}

		void Update() {
			if (IsTurboOn ()) {
				trail.enabled = true;
			} else {
				trail.enabled = false;
			}
		}
	}
}