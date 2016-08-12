using UnityEngine;
using System.Collections;
using MultiPacMan.Player.Collision;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public abstract class PelletEater : MonoBehaviour {

		public delegate bool IsCollidingWithPellet();
		public IsCollidingWithPellet collisionDelegate;

		public delegate PelletBehaviour GetPellet();
		public GetPellet getPelletDelegate;
		
		void Update() {
			if (collisionDelegate == null || getPelletDelegate == null) {
				return;
			}

			if (collisionDelegate()) {
				EatPellet(getPelletDelegate());
			}
		}

		protected abstract void EatPellet(PelletBehaviour pellet);
	}
}
