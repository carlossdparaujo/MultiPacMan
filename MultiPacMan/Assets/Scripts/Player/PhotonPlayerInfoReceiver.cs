using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public class PhotonPlayerInfoReceiver : MonoBehaviour, IPunObservable {
		
		public delegate void SetPlayerPosition(Vector2 position);
		public SetPlayerPosition positionDelegate;

		public delegate void SetPlayerTurboOn(bool isTurboOn);
		public SetPlayerTurboOn turboDelegate;

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if (positionDelegate == null || turboDelegate == null) {
				return;
			}

			if (stream.isWriting == false) {
				positionDelegate(DecompressPosition(stream.ReceiveNext()));
				turboDelegate(DecompressTurboFlag(stream.ReceiveNext()));
			}
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual Vector2 DecompressPosition(object data) {
			return (Vector2) data;
		}

		protected virtual bool DecompressTurboFlag(object data) {
			return (bool) data;
		}
	}
}

