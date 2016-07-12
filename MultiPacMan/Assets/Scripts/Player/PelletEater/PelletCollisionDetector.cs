using UnityEngine;
using System.Collections;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player.Collision
{
	public class PelletCollisionDetector : MonoBehaviour {

		private PelletBehaviour pellet = null;

		public bool IsCollidingWithPellet() {
			return (pellet != null);
		}

		public PelletBehaviour GetPellet() {
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
				pellet = other.gameObject.GetComponent<PelletBehaviour>();
			}
		}

		void OnTriggerExit2D(Collider2D other) {
			if (IsCollidingWithPellet(other)) {
				pellet = null;
			}
		}

		private bool IsCollidingWithPellet(Collider2D other) {
			return other.tag == "Pellet" && other.gameObject.GetComponent<PelletBehaviour>() != null;
		}
	}
}