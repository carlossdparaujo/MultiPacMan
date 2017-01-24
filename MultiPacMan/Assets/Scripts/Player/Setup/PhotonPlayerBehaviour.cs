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

			scoreSerializer = Add<PhotonPlayerScoreSerializer>();

			PhotonPelletEater pelletEater = Add<PhotonPelletEater>();
			pelletEater.eatPelletDelegate += (PelletBehaviour pellet) => {
				pellet.AnimatePelletEaten();

				int pelletValue = pellet.Score;
				int pelletId = pellet.Point.GetHashCode();

				RaiseEventOptions options = new RaiseEventOptions();
				options.CachingOption = EventCaching.DoNotCache;
				options.Receivers = ReceiverGroup.MasterClient;

				PhotonNetwork.RaiseEvent((byte) PhotonPelletEater.EAT_PELLET_EVENT_CODE, 
					new object[2] { pelletValue, pelletId }, 
					true, options
				);
			};

			PelletCollisionDetector collisionDetector = Add<PelletCollisionDetector>();
			collisionDetector.collisionDelegate += pelletEater.EatPellet;

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

