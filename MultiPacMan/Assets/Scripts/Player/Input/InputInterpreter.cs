using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player.Inputs
{
	public abstract class InputInterpreter : MonoBehaviour {
		public abstract bool IsTurboOn();
		public abstract Vector3 GetMovementDirection();
	}
}
