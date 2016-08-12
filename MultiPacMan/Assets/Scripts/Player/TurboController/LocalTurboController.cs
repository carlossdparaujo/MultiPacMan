using System;
using UnityEngine;

namespace MultiPacMan.Player.Turbo
{
	public class LocalTurboController : TurboController {
		
		public delegate bool IsTurboInUse();
		public IsTurboInUse turboDelegate;

		[SerializeField]
		private float turboCapacity = 100.0f;
		[SerializeField]
		private float currentTurboCapacity = 100.0f;
		[SerializeField]
		private float turboPerSecond = 30.0f;
		[SerializeField]
		private float turboRecoveryPerSecond = 10.0f;

		private bool isTurboOn = false;

		public override bool IsTurboOn() {
			return isTurboOn;
		}

		void Update() {
			if (turboDelegate == null) {
				return;
			}

			if (turboDelegate()) {
				float turboUsed = (turboPerSecond*Time.deltaTime);
				currentTurboCapacity = Mathf.Max(currentTurboCapacity - turboUsed, 0);
			} else {
				float turboRecovered = (turboRecoveryPerSecond*Time.deltaTime);
				currentTurboCapacity = Mathf.Min(currentTurboCapacity + turboRecovered, turboCapacity);
			}

			isTurboOn = turboDelegate() && (currentTurboCapacity > 0);
		}
	}
}

