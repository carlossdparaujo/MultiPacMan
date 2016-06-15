using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player.InputInterpreter
{
	public abstract class MovementInputInterpreter : MonoBehaviour {
		public abstract Vector3 GetMovementDirection();
	}
}
