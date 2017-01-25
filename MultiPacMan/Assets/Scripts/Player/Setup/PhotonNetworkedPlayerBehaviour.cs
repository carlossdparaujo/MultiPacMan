using System;
using UnityEngine;
using MultiPacMan.Player.Collision;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonNetworkedPlayerBehaviour : PhotonPlayer {
		
		void Start() {
			NetworkedMovementController movementController = Add<NetworkedMovementController>();

			PhotonPlayerInfoReceiver receiver = Add<PhotonPlayerInfoReceiver>();
			receiver.positionDelegate += movementController.UpdatePosition;

			PhotonPlayerScoreReceiver scoreReceiver = Add<PhotonPlayerScoreReceiver>();
			scoreReceiver.scoreDelegate += UpdateScore;

			PelletEater pelletEater = Add<PelletEater>();
			pelletEater.eatPelletDelegate += (PelletBehaviour pellet) => {
				pellet.AnimatePelletEaten();
			};

			PelletCollisionDetector collisionDetector = Add<PelletCollisionDetector>();
			collisionDetector.collisionDelegate += pelletEater.EatPellet;

			GetPhotonView().ObservedComponents.Add(receiver);
		}
	}
}

