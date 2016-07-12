using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PlayerBehaviour))]
	public class PhotonPlayerSerializer : MonoBehaviour, IPunObservable {

		private PlayerBehaviour player = null;
		private Rigidbody2D rb = null;

		void Start() {
			rb = GetComponent<Rigidbody2D>();
			player = GetComponent<PlayerBehaviour>();
		}

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if (stream.isWriting) {
				if (rb == null) {
					return;
				}

				stream.SendNext(CompressPosition(rb.position));
			} else {
				if (player == null) {
					return;
				}

				player.PlayerPosition = DecompressPosition(stream.ReceiveNext());
			}
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual object CompressPosition(Vector2 data) {
			return data;
		}

		protected virtual Vector2 DecompressPosition(object data) {
			return (Vector2) data;
		}
	}
}
