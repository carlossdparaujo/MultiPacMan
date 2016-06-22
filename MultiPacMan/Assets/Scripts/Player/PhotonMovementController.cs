using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PhotonPlayerSerializer))]
	public class PhotonMovementController : MovementController {

		private PhotonPlayerSerializer serializer;

		public override void Start() {
			base.Start();
			serializer = GetComponent<PhotonPlayerSerializer>();
		}

		void Update() {
			rb.MovePosition(serializer.PlayerPosition);
		}
	}
}