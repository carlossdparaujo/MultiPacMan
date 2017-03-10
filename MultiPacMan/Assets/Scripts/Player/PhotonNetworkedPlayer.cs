using System;
using UnityEngine;
using MultiPacMan.Player.Collision;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonNetworkedPlayer : PhotonPlayer {

		public override TurboController TurboController {
			get {
				return GetComponent<NetworkedTurboController>();
			}
		}

		void Start() {
			NetworkedMovementController movementController = Add<NetworkedMovementController>();
			NetworkedTurboController turboController = Add<NetworkedTurboController>();

			PhotonPlayerInfoReceiver receiver = Add<PhotonPlayerInfoReceiver>();
			receiver.positionDelegate += movementController.UpdatePosition;
			receiver.turboDelegate += turboController.UpdateTurbo;

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

