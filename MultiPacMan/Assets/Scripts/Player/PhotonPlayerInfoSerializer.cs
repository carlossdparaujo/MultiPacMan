using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public class PhotonPlayerInfoSerializer : MonoBehaviour, IPunObservable {

		public delegate Vector2 GetPlayerPosition();
		public GetPlayerPosition positionDelegate;

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if (positionDelegate == null) {
				return;
			}

			if (stream.isWriting) {
				stream.SendNext(CompressPosition(positionDelegate()));
			}
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual object CompressPosition(Vector2 data) {
			return data;
		}
	}
}
