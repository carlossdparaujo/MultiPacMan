using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player.Turbo
{
	public class NetworkedTurboController : TurboController {
		public delegate bool IsTurboInUse();
		public IsTurboInUse turboDelegate;

		private bool isTurboOn = false;

		public override bool IsTurboOn() {
			return isTurboOn;
		}

		void Update() {
			if (turboDelegate == null) {
				return;
			}

			if (turboDelegate()) {
				
			} else {
				
			}

			isTurboOn = turboDelegate ();
		}
	}
}