using System;
using UnityEngine;

namespace MultiPacMan.Player.Turbo
{
	public class NetworkedTurboController : TurboController {

		private bool isTurboOn = false;

		public void SetTurboOn(bool isTurboOn) {
			this.isTurboOn = isTurboOn;
		}

		public override bool IsTurboOn() {
			return isTurboOn;
		}
	}
}

