using System;
using UnityEngine;
using MultiPacMan.Player.Turbo;

namespace MultiPacMan.Player
{
	public class PhotonNetworkedPlayerBehaviour : Photon.MonoBehaviour, IPlayer {
		
		public void Setup() {
			NetworkedMovementController movementController = Add<NetworkedMovementController>();

			NetworkedTurboController turboController = Add<NetworkedTurboController>();

			PhotonPlayerInfoReceiver receiver = Add<PhotonPlayerInfoReceiver>();
			receiver.positionDelegate += movementController.UpdatePosition;
			receiver.turboDelegate += turboController.SetTurboOn;

			this.photonView.ObservedComponents.Add(receiver);
		}

		private T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}
	}
}

