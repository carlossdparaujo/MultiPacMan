using System;
using UnityEngine;
using MultiPacMan.Player.Inputs;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Player.Collision;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonPlayerBehaviour : IPlayer {
			
		public override void Setup() {
			DesktopInputInterpreter inputInterpreter = Add<DesktopInputInterpreter>();

			LocalTurboController turboController = Add<LocalTurboController>();
			turboController.turboDelegate += inputInterpreter.IsTurboOn;

			LocalMovementController movementController = Add<LocalMovementController>();
			movementController.directionDelegate += inputInterpreter.GetMovementDirection;
			movementController.turboDelegate += turboController.IsTurboOn;

			PelletCollisionDetector collisionDetector = Add<PelletCollisionDetector>();

			PhotonPlayerScoreSerializer scoreSerializer = Add<PhotonPlayerScoreSerializer>();

			PhotonPelletEater pelletEater = Add<PhotonPelletEater>();
			pelletEater.collisionDelegate += collisionDetector.IsCollidingWithPellet;
			pelletEater.getPelletDelegate += collisionDetector.GetPellet;
			pelletEater.eatPelletDelegate += (int value) => {
				AddToScore(value);
				scoreSerializer.UpdateScore(GetScore());
			};

			PhotonPlayerInfoSerializer serializer = Add<PhotonPlayerInfoSerializer>();
			serializer.positionDelegate += movementController.GetPosition;

			this.photonView.ObservedComponents.Add(serializer);
		}
	}
}

