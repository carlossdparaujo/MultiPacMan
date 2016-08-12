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

		public delegate void DidEatPellet(int pelletValue);
		public DidEatPellet eatPelletDelegate;
		
		void Update() {
			if (collisionDelegate == null || getPelletDelegate == null || eatPelletDelegate == null) {
				return;
			}

			if (collisionDelegate()) {
				PelletBehaviour pellet = getPelletDelegate();

				EatPellet(pellet);
			}
		}

		protected abstract void EatPellet(PelletBehaviour pellet);
	}
}
