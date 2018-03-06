using System;
using UnityEngine;
using MultiPacMan.Game;
using MultiPacMan.Photon.Player.SerializersAndReceivers;
using MultiPacMan.Player;
using MultiPacMan.Player.Movement;
using MultiPacMan.Player.Input;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Player.PelletEater;

using MultiPacMan.Pellet;

namespace MultiPacMan.Photon.Player
{
	public class PhotonLocalPlayer : PhotonPlayer {
			
		private PhotonPlayerScoreSerializer scoreSerializer;

		public override TurboController TurboController {
			get {
				return GetComponent<LocalTurboController>();
			}
		}

		void Start() {
			DesktopInputInterpreter inputInterpreter = Add<DesktopInputInterpreter>();

			LocalTurboController turboController = Add<LocalTurboController>();
			turboController.turboDelegate += inputInterpreter.IsTurboOn;

			LocalMovementController movementController = Add<LocalMovementController>();
			movementController.directionDelegate += inputInterpreter.GetMovementDirection;
			movementController.turboDelegate += turboController.IsTurboOn;

			SpriteDirectionChanger.directionDelegate += inputInterpreter.GetMovementDirection;

			scoreSerializer = Add<PhotonPlayerScoreSerializer>();

			PelletEater pelletEater = Add<PelletEater>();
			pelletEater.eatPelletDelegate += (PelletBehaviour pellet) => {
				pellet.AnimatePelletEaten();

				int pelletId = pellet.Point.GetHashCode();

				RaiseEventOptions options = new RaiseEventOptions();
				options.CachingOption = EventCaching.DoNotCache;
				options.Receivers = ReceiverGroup.MasterClient;

				PhotonNetwork.RaiseEvent((byte) GameController.EAT_PELLET_EVENT_CODE, 
					new object[1] { pelletId }, 
					true, options
				);
			};

			PelletCollisionDetector collisionDetector = Add<PelletCollisionDetector>();
			collisionDetector.collisionDelegate += pelletEater.EatPellet;

			PhotonPlayerInfoSerializer serializer = Add<PhotonPlayerInfoSerializer>();
			serializer.positionDelegate += movementController.GetPosition;
			serializer.velocityDelegate += movementController.GetVelocity;

			GetPhotonView().ObservedComponents.Add(serializer);
		}

		void OnPhotonPlayerConnected(PhotonPlayer player) {
			scoreSerializer.UpdateScore(Score);
		}
	}
}

