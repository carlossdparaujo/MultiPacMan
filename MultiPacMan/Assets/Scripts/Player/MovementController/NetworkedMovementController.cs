using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public class NetworkedMovementController : MovementController {

		public void UpdatePosition(Vector2 position) {
			rb.MovePosition(position);
		}
	}
}