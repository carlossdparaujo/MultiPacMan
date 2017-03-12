using System;
using UnityEngine;
using MultiPacMan.Player.Inputs;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Player.Collision;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonLocalPlayer : PhotonPlayer {
			
		private PhotonPlayerScoreSerializer scoreSerializer;

		public static int EAT_PELLET_EVENT_CODE = 1;
		public static int REMOVE_PELLET_EVENT_CODE = 2;

		public override TurboController TurboController {
			get {
				return GetComponent<LocalTurboController>();
			}
		}

		void Start() {
			PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;

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

				int pelletValue = pellet.Score;
				int pelletId = pellet.Point.GetHashCode();

				RaiseEventOptions options = new RaiseEventOptions();
				options.CachingOption = EventCaching.DoNotCache;
				options.Receivers = ReceiverGroup.MasterClient;

				PhotonNetwork.RaiseEvent((byte) EAT_PELLET_EVENT_CODE, 
					new object[2] { pelletValue, pelletId }, 
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

		public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
			if ((int) eventCode == EAT_PELLET_EVENT_CODE) {
				object[] data = (object[]) content;

				int pelletScore = (int) data[0];
				int pelletId = (int) data[1];

				if (PhotonNetwork.isMasterClient) {
					RaiseEventOptions options = new RaiseEventOptions();
					options.CachingOption = EventCaching.AddToRoomCacheGlobal;
					options.Receivers = ReceiverGroup.All;

					PhotonNetwork.RaiseEvent((byte) REMOVE_PELLET_EVENT_CODE, 
						new object[3] { pelletScore, pelletId, senderId }, 
						true, options
					);
				}
			}
		}

		void OnPhotonPlayerConnected(PhotonPlayer player) {
			scoreSerializer.UpdateScore(Score);
		}
	}
}

