using UnityEngine;
using System.Collections;
using MultiPacMan.Player.Collision;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PelletCollisionDetector))]
	public abstract class PelletEater : MonoBehaviour {

		private PelletCollisionDetector detector;

		void Start() {
			detector = GetComponent<PelletCollisionDetector>();
		}
		
		void Update() {
			if (detector.IsCollidingWithPellet()) {
				EatPellet(detector.GetPellet());
			}
		}

		protected abstract void EatPellet(PelletBehaviour pellet);
	}
}
