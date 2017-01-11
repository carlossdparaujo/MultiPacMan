using System;
using UnityEngine;
using MultiPacMan.Player.Inputs;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Player.Collision;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonPlayerBehaviour : IPlayer {
			
		private LocalTurboController turboController;
		private PhotonPlayerScoreSerializer scoreSerializer;

		public override void Setup() {
			DesktopInputInterpreter inputInterpreter = Add<DesktopInputInterpreter>();

			turboController = Add<LocalTurboController>();
			turboController.turboDelegate += inputInterpreter.IsTurboOn;

			LocalMovementController movementController = Add<LocalMovementController>();
			movementController.directionDelegate += inputInterpreter.GetMovementDirection;
			movementController.turboDelegate += turboController.IsTurboOn;

			PelletCollisionDetector collisionDetector = Add<PelletCollisionDetector>();

			scoreSerializer = Add<PhotonPlayerScoreSerializer>();

			PhotonPelletEater pelletEater = Add<PhotonPelletEater>();
			pelletEater.collisionDelegate += collisionDetector.IsCollidingWithPellet;
			pelletEater.getPelletDelegate += collisionDetector.GetPellet;

			// HACK: Isso precisa existir, mas para o caso networkado não existe essa dependência
			pelletEater.eatPelletDelegate += (int value) => {
			};

			PhotonPlayerInfoSerializer serializer = Add<PhotonPlayerInfoSerializer>();
			serializer.positionDelegate += movementController.GetPosition;
			serializer.velocityDelegate += movementController.GetVelocity;

			this.photonView.ObservedComponents.Add(serializer);
		}

		public float GetTurboFuelPercentage() {
			return turboController.GetTurboFuelPercentage();
		}

		void OnPhotonPlayerConnected(PhotonPlayer player) {
			scoreSerializer.UpdateScore(Score);
		}
	}
}

