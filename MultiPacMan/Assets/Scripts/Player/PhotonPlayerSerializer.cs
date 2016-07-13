using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PlayerBehaviour))]
	public class PhotonPlayerSerializer : MonoBehaviour, IPunObservable {

		private PlayerBehaviour player = null;

		void Start() {
			player = GetComponent<PlayerBehaviour>();
		}

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if (player == null) {
				return;
			}

			if (stream.isWriting) {
				stream.SendNext(CompressPosition(player.PlayerPosition));
				stream.SendNext(CompressTurboFlag(player.IsTurboOn));
			} else {
				player.PlayerPosition = DecompressPosition(stream.ReceiveNext());
				player.IsTurboOn = DecompressTurboFlag(stream.ReceiveNext());
			}
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual object CompressPosition(Vector2 data) {
			return data;
		}

		protected virtual Vector2 DecompressPosition(object data) {
			return (Vector2) data;
		}

		protected virtual object CompressTurboFlag(bool data) {
			return data;
		}

		protected virtual bool DecompressTurboFlag(object data) {
			return (bool) data;
		}
	}
}
