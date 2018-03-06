using UnityEngine;
using System.Collections;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player.PelletEater
{
	public class PelletCollisionDetector : MonoBehaviour {

		public delegate void CollidingWithPellet(PelletBehaviour pellet);
		public CollidingWithPellet collisionDelegate;

		void OnTriggerEnter2D(Collider2D other) {
			DetectCollision(other);
		}

		private void DetectCollision(Collider2D other) {
			if (IsCollidingWithPellet(other)) {
				PelletBehaviour pellet = other.gameObject.GetComponent<PelletBehaviour>();
				collisionDelegate(pellet);
			}
		}

		private bool IsCollidingWithPellet(Collider2D other) {
			return other.tag == "Pellet" && other.gameObject.GetComponent<PelletBehaviour>() != null;
		}
	}
}