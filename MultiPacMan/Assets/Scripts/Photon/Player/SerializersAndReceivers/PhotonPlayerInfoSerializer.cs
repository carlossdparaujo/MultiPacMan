using UnityEngine;
using System.Collections;

namespace MultiPacMan.Photon.Player.SerializersAndReceivers
{
	public class PhotonPlayerInfoSerializer : MonoBehaviour, IPunObservable {

		public delegate Vector2 GetPlayerPosition();
		public GetPlayerPosition positionDelegate;

		public delegate Vector2 GetPlayerVelocity();
		public GetPlayerVelocity velocityDelegate;

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if (positionDelegate == null) {
				return;
			}

			if (stream.isWriting) {
				stream.SendNext(CompressPosition(positionDelegate()));
				stream.SendNext(CompressVelocity(velocityDelegate()));
			}
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual object CompressPosition(Vector2 data) {
			return data;
		}

		protected virtual object CompressVelocity(Vector2 data) {
			return data;
		}
	}
}
