using UnityEngine;
using System.Collections;

public class PelletCollisionDetector : MonoBehaviour {

	private GameObject pellet = null;

	public bool IsCollidingWithPellet() {
		return (pellet != null);
	}

	public GameObject GetPellet() {
		return pellet;
	}

	void OnTriggerEnter2D(Collider2D other) {
		DetectCollision(other);
	}

	void OnTriggerStay2D(Collider2D other) {
		DetectCollision(other);
	}

	private void DetectCollision(Collider2D other) {
		if (IsCollidingWithPellet(other)) {
			pellet = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (IsCollidingWithPellet(other)) {
			pellet = null;
		}
	}

	private bool IsCollidingWithPellet(Collider2D other) {
		return other.tag == "Pellet";
	}
}
