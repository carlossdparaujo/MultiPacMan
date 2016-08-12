using System;
using UnityEngine;
using MultiPacMan.Player.Turbo;

namespace MultiPacMan.Player
{
	public class PhotonNetworkedPlayerBehaviour : IPlayer {
		
		public override void Setup() {
			NetworkedMovementController movementController = Add<NetworkedMovementController>();

			PhotonPlayerInfoReceiver receiver = Add<PhotonPlayerInfoReceiver>();
			receiver.positionDelegate += movementController.UpdatePosition;

			PhotonPlayerScoreReceiver scoreReceiver = Add<PhotonPlayerScoreReceiver>();
			scoreReceiver.scoreDelegate += UpdateScore;

			this.photonView.ObservedComponents.Add(receiver);
		}
	}
}

