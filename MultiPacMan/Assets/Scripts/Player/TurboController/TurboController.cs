using System;
using UnityEngine;
using MultiPacMan.Player.Inputs;

namespace MultiPacMan.Player.Turbo
{
	[RequireComponent(typeof(InputInterpreter))]
	public class TurboController : MonoBehaviour {

		[SerializeField]
		private float turboCapacity = 100.0f;
		[SerializeField]
		private float currentTurboCapacity = 100.0f;
		[SerializeField]
		private float turboPerSecond = 30.0f;
		[SerializeField]
		private float turboRecoveryPerSecond = 10.0f;

		private bool isTurboOn = false;
		public bool IsTurboOn {
			get {
				return isTurboOn;
			}
		}

		private InputInterpreter inputInterpreter;

		void Start() {
			inputInterpreter = GetComponent<InputInterpreter>();
		}

		void Update() {
			if (inputInterpreter.IsTurboOn()) {
				float turboUsed = (turboPerSecond*Time.deltaTime);
				currentTurboCapacity = Mathf.Max(currentTurboCapacity - turboUsed, 0);
			} else {
				float turboRecovered = (turboRecoveryPerSecond*Time.deltaTime);
				currentTurboCapacity = Mathf.Min(currentTurboCapacity + turboRecovered, turboCapacity);
			}

			isTurboOn = inputInterpreter.IsTurboOn() && (currentTurboCapacity > 0);
		}
	}
}

