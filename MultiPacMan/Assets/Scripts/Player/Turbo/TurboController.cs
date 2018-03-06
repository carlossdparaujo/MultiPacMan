using System;
using UnityEngine;

namespace MultiPacMan.Player.Turbo
{
	public class TurboController : MonoBehaviour {
		protected TrailRenderer trail;
		void Start() {
			trail = GetComponentInChildren<TrailRenderer>();
			trail.enabled = false;
		}

		public virtual bool IsTurboOn() { return false; }
		public virtual float GetTurboFuelPercentage() { return 0.0f; }
	}
}

