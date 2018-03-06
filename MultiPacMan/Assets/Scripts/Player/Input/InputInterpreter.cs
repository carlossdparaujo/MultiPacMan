using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player.Input
{
	public abstract class InputInterpreter : MonoBehaviour {
		public abstract bool IsTurboOn();
		public abstract Vector2 GetMovementDirection();
	}
}
