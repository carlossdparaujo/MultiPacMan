using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public class PhotonPlayerInfoSerializer : MonoBehaviour, IPunObservable {

		public delegate Vector2 GetPlayerPosition();
		public GetPlayerPosition positionDelegate;

		public delegate bool IsPlayerTurboOn();
		public IsPlayerTurboOn turboDelegate;

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if (positionDelegate == null || turboDelegate == null) {
				return;
			}

			if (stream.isWriting) {
				stream.SendNext(CompressPosition(positionDelegate()));
				stream.SendNext(CompressTurboFlag(turboDelegate()));
			}
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual object CompressPosition(Vector2 data) {
			return data;
		}

		protected virtual object CompressTurboFlag(bool data) {
			return data;
		}
	}
}
