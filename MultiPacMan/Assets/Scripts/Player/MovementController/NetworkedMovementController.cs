using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PlayerBehaviour))]
	public class NetworkedMovementController : MovementController {

		private PlayerBehaviour player;

		public override void Start() {
			base.Start();
			player = GetComponent<PlayerBehaviour>();
		}

		void Update() {
			rb.MovePosition(player.PlayerPosition);
		}
	}
}