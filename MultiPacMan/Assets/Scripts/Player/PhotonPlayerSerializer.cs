using UnityEngine;
using System.Collections;

public class PhotonPlayerSerializer : MonoBehaviour, IPunObservable {

	private Vector2 playerPosition;
	public Vector2 PlayerPosition {
		get {
			return playerPosition;
		}
	}

	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext(CompressPosition(rb.position));
		} else {
			playerPosition = DecompressPosition(stream.ReceiveNext());
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
