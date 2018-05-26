using System;
using UnityEngine;
using MultiPacMan.Photon.Player.SerializersAndReceivers;
using MultiPacMan.Player;
using MultiPacMan.Player.Movement;
using MultiPacMan.Player.PelletEater;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Pellet;

namespace MultiPacMan.Photon.Player
{
	public class PhotonNetworkedPlayer : PhotonBasePlayer {

		public override TurboController TurboController {
			get {
				return GetComponent<NetworkedTurboController>();
			}
		}

		void Start() {
			NetworkedMovementController movementController = Add<NetworkedMovementController>();
			NetworkedTurboController turboController = Add<NetworkedTurboController>();
			turboController.getVelocityDelegate += movementController.GetVelocity;

			SpriteDirectionChanger.directionDelegate += movementController.GetDirection;

			PhotonPlayerInfoReceiver receiver = Add<PhotonPlayerInfoReceiver>();
			receiver.positionDelegate += movementController.UpdatePosition;

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

