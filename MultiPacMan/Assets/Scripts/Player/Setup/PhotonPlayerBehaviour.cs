using System;
using UnityEngine;
using MultiPacMan.Player.Inputs;
using MultiPacMan.Player.Turbo;
using MultiPacMan.Player.Collision;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonPlayerBehaviour : Photon.MonoBehaviour, IPlayer {
			
		public void Setup() {
			DesktopInputInterpreter inputInterpreter = Add<DesktopInputInterpreter>();

			LocalTurboController turboController = Add<LocalTurboController>();
			turboController.turboDelegate += inputInterpreter.IsTurboOn;

			LocalMovementController movementController = Add<LocalMovementController>();
			movementController.directionDelegate += inputInterpreter.GetMovementDirection;
			movementController.turboDelegate += turboController.IsTurboOn;

			PelletCollisionDetector collisionDetector = Add<PelletCollisionDetector>();

			PhotonPelletEater pelletEater = Add<PhotonPelletEater>();
			pelletEater.collisionDelegate += collisionDetector.IsCollidingWithPellet;
			pelletEater.getPelletDelegate += collisionDetector.GetPellet;

			PhotonPlayerInfoSerializer serializer = Add<PhotonPlayerInfoSerializer>();
			serializer.positionDelegate += movementController.GetPosition;
			serializer.turboDelegate += turboController.IsTurboOn;

			this.photonView.ObservedComponents.Add(serializer);
		}

		private T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}
	}
}

